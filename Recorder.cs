using System.IO;
using System.Drawing;
using System.Timers;
using System.Drawing.Imaging;

class Recorder
{
    public Rectangle region;
    public float fps = 25;
    public float currentFps { get; private set; }
    public int duration { get; private set; }
    public int frames { get; private set; }
    public string tempDir { get; private set; }
    public bool isRecording {
        get { return captureTimer.Enabled; }
        private set { }
    }
    
    private Timer durationTimer = new Timer();
    private Timer captureTimer = new Timer();

    public bool Start()
    {
        if (captureTimer.Enabled)
            return false;

        // Reset
        frames = 1;

        // Create Temporary Directory
        tempDir = CreateTempDirectory();

        // Set Duration 
        durationTimer.Interval = 1000;
        durationTimer.Elapsed += new ElapsedEventHandler(DurationTick);

        // Set Capture Timer
        captureTimer.Interval = 1000 / fps;
        captureTimer.Elapsed += new ElapsedEventHandler(CaptureTick);

        // Enable Timers
        durationTimer.Enabled = true;
        captureTimer.Enabled = true;
        isRecording = true;

        return isRecording;
    }

    public bool Stop()
    {
        if (!captureTimer.Enabled)
            return false;

        // Set Timers
        durationTimer.Enabled = false;
        captureTimer.Enabled = false;
        isRecording = false ;
        return isRecording;
    }

    public bool Flush()
    {
        if (!captureTimer.Enabled)
            return false;

        File.Delete(tempDir);
        return File.Exists(tempDir);
    }

    private void DurationTick(object sender, ElapsedEventArgs e)
    {
        duration++;
        currentFps = frames / duration;
    }

    private void CaptureTick(object sender, ElapsedEventArgs e)
    {
        Bitmap bmp = Capture();
        try
        {
            // If saving was faster, we would be able to achieve higher FPS
            // the bottleneck does not seem to be CopyFromScreen
            bmp.Save(Path.Combine(tempDir, frames.ToString() + ".png"), ImageFormat.Png);
            frames++;
        }
        catch
        {
            // Suppress
        }
        bmp.Dispose();
    }

    public Bitmap Capture()
    {
        var bmp = new Bitmap(region.Width - 2, region.Height - 2, PixelFormat.Format32bppRgb);
        var graphics = Graphics.FromImage(bmp);
        graphics.CopyFromScreen(region.X, region.Y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
        graphics.Dispose();
        return bmp;
    }

    public string CreateTempDirectory()
    {
        string tempDir = Path.GetTempFileName();
        File.Delete(tempDir);
        Directory.CreateDirectory(tempDir);
        return tempDir;
    }
}