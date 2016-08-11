using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using NAudio.Wave;
using System.Threading.Tasks;

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
    public bool isPaused { get; private set; }

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

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="imageFormat">Image Format to save images as</param>
    public Recorder(ImageFormat imageFormat = null)
    {
        if (imageFormat == null)
            return;

        this.imageFormat = imageFormat;
        this.imageExtension = "." + imageFormat.ToString().ToLower();
    }

    /// <summary>
    /// Start Recording
    /// </summary>
    /// <param name="recordAudio">Record Audio</param>
    /// <returns>Successful</returns>
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
            audioFile = new WaveFileWriter(Path.Combine(tempPath, "audio.wav"), audioSource.WaveFormat);

            audioSource.StartRecording();
        }

        // Start Timers
        StartTimers();

        status = "Recording";
        isRecording = true;
        return isRecording;
    }

    /// <summary>
    /// Stop Recording
    /// </summary>
    /// <returns>Successful</returns>
    public bool Stop()
    {
        if (!isRecording)
            return false;

        // Stop Capturing Audio
        if (recordAudio)
        {
            audioSource.StopRecording();

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

        // Kill Timers
        StopTimers();

        status = "Idle";
        isRecording = false;
        return isRecording;
    }

    /// <summary>
    /// Toggle recording to pause or resume if paused
    /// </summary>
    /// <returns>Successful</returns>
    public bool Pause()
    {
        if (!isRecording)
            return false;

        // Paused; needs to be resumed
        if (isPaused)
        {
            // Resume Audio
            if(recordAudio)
                audioSource.StartRecording();
            StartTimers();
            status = "Recording";
        }

        // Recording; needs to be paused
        else
        {
            // Pause Audio
            if (recordAudio)
                audioSource.StopRecording();
            StopTimers();
            status = "Paused";
        }

        isPaused = !isPaused;
        return true;
    }

    /// <summary>
    /// Redirects to Pause()
    /// </summary>
    /// <returns>Successful</returns>
    public bool Resume()
    {
        return Pause();
    }

    /// <summary>
    /// Start Recording Timers
    /// </summary>
    private void StartTimers()
    {
        // Enable Capture Timer
        durationTimerDelegate = new PInvoke.MMTimerProc(DurationTick);
        durationTimerId = PInvoke.timeSetEvent(100, 0, durationTimerDelegate, 0, 1);

        // Enable Duration Timer
        captureTimerDelegate = new PInvoke.MMTimerProc(CaptureTick);
        captureTimerId = PInvoke.timeSetEvent((uint)(1000 / fps), 0, captureTimerDelegate, 0, 1);
    }

    /// <summary>
    /// Stop Recording Timers
    /// </summary>
    private void StopTimers()
    {
        // Kill Timers
        PInvoke.timeKillEvent(captureTimerId);
        PInvoke.timeKillEvent(durationTimerId);

        // Nullify
        captureTimerId = 0;
        captureTimerDelegate = null;
        durationTimerId = 0;
        durationTimerDelegate = null;
    }

    /// <summary>
    /// Tick once a second second, update duration and current FPS
    /// </summary>
    private void DurationTick(uint timerid, uint msg, IntPtr user, uint dw1, uint dw2)
    {
        duration += (float).1;
        averageFps = frames / duration;
    }

    /// <summary>
    /// Capture and save file every (1000 / fps)
    /// </summary>
    private void CaptureTick(uint timerid, uint msg, IntPtr user, uint dw1, uint dw2)
    {
        // TODO: Make this write to a single file, rather than multiple little ones
        // as the little ones will always be slower than a single file. After the recording,
        // the single file can be expanded into multiple (when FS performance is less needed) for ffmpeg
        Task.Run(() =>
        {
            var bmp = Capture();
            bmp.Save(Path.Combine(tempPath, "_" + frames.ToString() + imageExtension), imageFormat);
            bmp.Dispose();
            frames++;
        });
    }

    /// <summary>
    /// Write captured Audio to file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void WriteAudio(object sender, WaveInEventArgs e)
    {
        if (audioFile == null)
            return;

        audioFile.Write(e.Buffer, 0, e.BytesRecorded);
        audioFile.Flush();
    }

    /// <summary>
    /// Capture screenshot
    /// </summary>
    /// <returns>Captured bitmap of region area</returns>
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
                        0, 0, 0, IntPtr.Zero, 0x0003);
                    graphics.ReleaseHdc();
                }
        }

        // Release
        graphics.Dispose();
        return bmp;
    }

    /// <summary>
    /// Create temporary directory to store the images
    /// </summary>
    public void CreateTemporaryPath()
    {
        string tempPath = Path.GetTempFileName() + "_WebMCam";
        Directory.CreateDirectory(tempPath);
        this.tempPath = tempPath;
    }

    /// <summary>
    /// Delete Temp Folder
    /// </summary>
    /// <returns>Successful</returns>
    public bool Flush()
    {
        Directory.Delete(tempPath, true);
        return !Directory.Exists(tempPath);
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