using System;
using NAudio.Wave;

namespace WebMCam
{
	class Audio_Capture
	{
		public WasapiLoopbackCapture Source = null;
		public WaveFileWriter File = null;
		public bool Recording = false;

		public void Start(string path)
		{
			Source = new WasapiLoopbackCapture();

			Source.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
			Source.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

			File = new WaveFileWriter(path, Source.WaveFormat);

			Source.StartRecording();
			Recording = true;
		}

		public void Stop()
		{
			try
			{
				Source.StopRecording();
			}
			catch { }
			Recording = false;
		}

		void waveSource_DataAvailable(object sender, WaveInEventArgs e)
		{
			if (File != null)
			{
				File.Write(e.Buffer, 0, e.BytesRecorded);
				File.Flush();
			}
		}

		void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
		{
			if (Source != null)
			{
				Source.Dispose();
				Source = null;
			}

			if (File != null)
			{
				File.Dispose();
				File = null;
			}
		}
	}
}
