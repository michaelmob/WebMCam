using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using NAudio.Wave;

class Recorder
{
    // Public Settings
    public Rectangle region;
    public string status = "Idle";
    public float fps = 30;
    public bool drawCursor = true;

    // Public Information
    public float averageFps { get; private set; }
    public float duration { get; private set; }
    public int frames { get; private set; }
    public string tempPath { get; private set; }
    public bool isRecording { get; private set; }

    // Image Capturing
    private ImageFormat imageFormat = ImageFormat.Png;
    private string imageExtension = ".png";

    // Timers
    uint durationTimerId;
    uint captureTimerId;
    PInvoke.MMTimerProc durationTimerDelegate;
    PInvoke.MMTimerProc captureTimerDelegate;

    // Audio Capturing
    private bool recordAudio;
    private WasapiLoopbackCapture audioSource;
    private WaveFileWriter audioFile;

    /* Constructor -- Set Image format for Saving */
    public Recorder(ImageFormat imageFormat = null)
    {
        if (imageFormat == null)
            return;

        this.imageFormat = imageFormat;
        this.imageExtension = "." + imageFormat.ToString().ToLower();
    }

    /* Start Capturing */
    public bool Start(bool recordAudio = false)
    {
        if (isRecording)
            return false;

        // Reset
        status = "Pending";
        frames = 0;

        // Create Temporary Directory
        CreateTemporaryPath();

        // Setup Audio Recording
        if (recordAudio)
        {
            this.recordAudio = recordAudio;
            audioSource = new WasapiLoopbackCapture();
            audioSource.DataAvailable += new EventHandler<WaveInEventArgs>(WriteAudio);
            audioSource.RecordingStopped += new EventHandler<StoppedEventArgs>(RecordingStopped);
            audioFile = new WaveFileWriter(Path.Combine(tempPath, "audio.wav"), audioSource.WaveFormat);
        }

        // Enable Capture Timer
        durationTimerDelegate = new PInvoke.MMTimerProc(DurationTick);
        durationTimerId = PInvoke.timeSetEvent(100, 0, durationTimerDelegate, 0, 1);

        // Enable Duration Timer
        captureTimerDelegate = new PInvoke.MMTimerProc(CaptureTick);
        captureTimerId = PInvoke.timeSetEvent((uint)(1000 / fps), 0, captureTimerDelegate, 0, 1);

        if (recordAudio)
            audioSource.StartRecording();

        status = "Recording";
        isRecording = true;
        return isRecording;
    }

    /* Stop Capturing */
    public bool Stop()
    {
        if (!isRecording)
            return false;

        // Stop Capturing Audio
        if(recordAudio)
            audioSource.StopRecording();

        // Kill Timers
        PInvoke.timeKillEvent(captureTimerId);
        PInvoke.timeKillEvent(durationTimerId);
        
        captureTimerId = 0;
        captureTimerDelegate = null;
        durationTimerId = 0;
        durationTimerDelegate = null;

        status = "Idle";
        isRecording = false;
        return isRecording;
    }

    /* Delete Temp Folder */
    public bool Flush()
    {
        Directory.Delete(tempPath, true);
        return !File.Exists(tempPath);
    }

    /* Tick once a second second, update duration and current FPS */
    private void DurationTick(uint timerid, uint msg, IntPtr user, uint dw1, uint dw2)
    {
        duration += (float).1;
        averageFps = frames / duration;
        Console.WriteLine(averageFps);
    }

    /* Capture and save file every (1000 / fps) */
    private void CaptureTick(uint timerid, uint msg, IntPtr user, uint dw1, uint dw2)
    {
        var bmp = Capture();
        frames++;

        new System.Threading.Thread(delegate () {
            try
            {
                bmp.Save(Path.Combine(tempPath, "_" + frames.ToString() + imageExtension), imageFormat);
                bmp.Dispose();
            } catch { }
        }).Start();
    }

    /* Write captured Audio to file */
    void WriteAudio(object sender, WaveInEventArgs e)
    {
        if (audioFile == null)
            return;

        audioFile.Write(e.Buffer, 0, e.BytesRecorded);
        audioFile.Flush();
    }

    /* Dispose of audioSource and audioFile on finish recording */
    void RecordingStopped(object sender, StoppedEventArgs e)
    {
        if (audioSource != null)
        {
            audioSource.Dispose();
            audioSource = null;
        }

        if (audioFile != null)
        {
            audioFile.Dispose();
            audioFile = null;
        }
    }

    /* Capture screenshot */
    public Bitmap Capture()
    {
        // Create Bitmap and drawing surface from the bmp variable
        // this will allow CopyFromScreen to "copy" the screen to the variable's address
        var bmp = new Bitmap(region.Width - 2, region.Height - 2, PixelFormat.Format32bppRgb);
        var graphics = Graphics.FromImage(bmp);

        // Capture the Screenshot and save it to bmp
        graphics.CopyFromScreen(region.X, region.Y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

        // Draw Cursor
        if (drawCursor)
        {
            PInvoke.CursorInfo cursorInfo;
            cursorInfo.cbSize = Marshal.SizeOf(typeof(PInvoke.CursorInfo));

            if (PInvoke.GetCursorInfo(out cursorInfo))
                if (cursorInfo.flags == 0x0001)
                {
                    var hdc = graphics.GetHdc();
                    PInvoke.DrawIconEx(hdc, cursorInfo.ptScreenPos.X - region.X,
                        cursorInfo.ptScreenPos.Y - region.Y, cursorInfo.hCursor,
                        0, 0, 0, IntPtr.Zero, 0x0003 );
                    graphics.ReleaseHdc();
                }
        }

        // Release
        graphics.Dispose();
        return bmp;
    }

    /* Create temporary directory to store the iamges */
    public void CreateTemporaryPath()
    {
        string tempPath = Path.GetTempFileName() + "_WebMCam";
        Directory.CreateDirectory(tempPath);
        this.tempPath = tempPath;
    }
}

public class PInvoke
{
    public delegate void MMTimerProc(uint timerid, uint msg, IntPtr user, uint dw1, uint dw2);

    [DllImport("winmm.dll")]
    public static extern uint timeSetEvent(uint uDelay, uint uResolution, [MarshalAs(UnmanagedType.FunctionPtr)] MMTimerProc lpTimeProc, uint dwUser, int fuEvent);

    [DllImport("winmm.dll")]
    public static extern uint timeKillEvent(uint uTimerID);

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo { public int cbSize; public int flags; public IntPtr hCursor; public Point ptScreenPos; }

    [DllImport("user32.dll")]
    public static extern bool GetCursorInfo(out CursorInfo pci);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

}