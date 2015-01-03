/*
 * Created by SharpDevelop.
 * User: Mike
 * Date: 1/3/2015
 * Time: 9:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.IO;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace WebMCam
{
	/// <summary>
	/// Description of Form_Output.
	/// </summary>
	public partial class Form_Output : Form
	{
		String temp_storage, ffmpeg_loc, save_file, arguments;
		Int16 frame_count;
		Process process = new Process();
		
		public Form_Output(String _temp_storage, String _ffmpeg_loc, String _save_file, String _arguments, Int16 _frame_count = 0)
		{
			// Assign class variables
			temp_storage = _temp_storage;
			ffmpeg_loc = _ffmpeg_loc;
			save_file = _save_file;
			arguments = _arguments;
			frame_count = _frame_count;
			
			InitializeComponent();
		}
		
		void Form_OutputLoad(object sender, EventArgs e)
		{
			
			// Redirect all info
			var ffmpeg = new ProcessStartInfo();
			
			ffmpeg.CreateNoWindow = true;
			ffmpeg.UseShellExecute = false;
			ffmpeg.RedirectStandardInput = true;
			ffmpeg.RedirectStandardOutput = true;
			ffmpeg.RedirectStandardError = true;
			
			ffmpeg.WorkingDirectory = temp_storage;
			ffmpeg.FileName = ffmpeg_loc;
			ffmpeg.Arguments = arguments;

			Debug.WriteLine(ffmpeg.FileName + " " + ffmpeg.Arguments);
			
			process.StartInfo = ffmpeg;
			
			process.OutputDataReceived += process_DataReceived;
			process.ErrorDataReceived += process_DataReceived;
			
			// Allow for files to finish saving, 0.5s will be barely noticeable
			Thread.Sleep(500);		
			
			process.Start();
			
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
			
		}
				
		private void process_DataReceived(object sender, DataReceivedEventArgs e)
		{
			Debug.WriteLine(e.Data);
			
			// Append text in a thread-safe matter
			output.Invoke(new MethodInvoker(
				delegate() {
					if (e.Data != null)
						output.AppendText(e.Data + Environment.NewLine);
				}
			));
		}
		
		void CancelClick(object sender, EventArgs e)
		{
			// If our process is not dead, kill it
			if (!process.HasExited) {
				process.Kill();
			}
			
			Close();
			Application.Restart();
		}
		
		// So we don't have to mess with invoking the progress_bar in DataRecieved
		void OutputTextChanged(object sender, EventArgs e)
		{
			// Get last line
			String line = output.Lines[output.Lines.Length - 2];
			
			// Parse line for progress
			if (line.StartsWith("frame=")) {
				progress_bar.Value = Convert.ToInt16(
					((float)Convert.ToInt16(line.Replace(" ", "").Substring(6).Split('f')[0]) / (float)frame_count) * 100
				);
			} else if (line.StartsWith("video:")) {
				progress_bar.Value = 100;
				open.Enabled = true;
				cancel.Text = "Done";
			}
		}
		
		void OpenClick(object sender, EventArgs e)
		{
			// Start our webm
			Process.Start(save_file);
		}
	}
}
