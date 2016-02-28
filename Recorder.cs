using System;
using System.IO;
using System.Drawing;
using System.Timers;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using NAudio.Wave;

class Recorder
{
    public Rectangle region;
    public float fps = 30;
    public float averageFps { get; private set; }
    public float duration { get; private set; }
    public int frames { get; private set; }
    public string tempPath { get; private set; }
    public bool drawCursor = true;
    public bool isRecording {
        get { return captureTimer.Enabled; }
        private set { }
    }

    private float fofSum = 0;
    private Timer durationTimer = new Timer();
    private Timer captureTimer = new Timer();
    private ImageFormat imageFormat = ImageFormat.Png;
    private string imageExtension = ".png";

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
        if (captureTimer.Enabled)
            return false;

        // Reset
        frames = 0;
        fofSum = 0;

        // Create Temporary Directory
        CreateTemporaryPath();

        // Set Duration 
        durationTimer.Interval = 100;
        durationTimer.Elapsed += new ElapsedEventHandler(DurationTick);

        // Set Capture Timer
        captureTimer.Interval = 800 / fps;
        captureTimer.Elapsed += new ElapsedEventHandler(CaptureTick);

        // Setup Audio Recording
        if (recordAudio)
        {
            this.recordAudio = recordAudio;
            audioSource = new WasapiLoopbackCapture();
            audioSource.DataAvailable += new EventHandler<WaveInEventArgs>(WriteAudio);
            audioSource.RecordingStopped += new EventHandler<StoppedEventArgs>(RecordingStopped);
            audioFile = new WaveFileWriter(Path.Combine(tempPath, "audio.wav"), audioSource.WaveFormat);
        }

        // Enable Timers
        durationTimer.Enabled = true;
        captureTimer.Enabled = true;

        if (recordAudio)
            audioSource.StartRecording();

        isRecording = true;

        return isRecording;
    }

    /* Stop Capturing */
    public bool Stop()
    {
        if (!captureTimer.Enabled)
            return false;

        if(recordAudio)
            audioSource.StopRecording();

        // Set Timers
        durationTimer.Enabled = false;
        captureTimer.Enabled = false;
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
    private void DurationTick(object sender, ElapsedEventArgs e)
    {
        duration += (float)0.1;
        averageFps = frames / duration;
    }

    void WriteAudio(object sender, WaveInEventArgs e)
    {
        if (audioFile == null)
            return;

        audioFile.Write(e.Buffer, 0, e.BytesRecorded);
        audioFile.Flush();
    }

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

    /* Capture and save file every (1000 / fps) */
    private void CaptureTick(object sender, ElapsedEventArgs e)
    {
        Bitmap bmp = Capture();
        frames++;

        // If saving was faster, we would be able to achieve higher FPS
        // the bottleneck does not seem to be CopyFromScreen
        bmp.Save(Path.Combine(tempPath, "_" + frames.ToString() + imageExtension), imageFormat);
        bmp.Dispose();
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
    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo { public int cbSize; public int flags; public IntPtr hCursor; public Point ptScreenPos; }

    [DllImport("user32.dll")]
    public static extern bool GetCursorInfo(out CursorInfo pci);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);
}