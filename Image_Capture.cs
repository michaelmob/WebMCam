using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WebMCam
{
	public static class Image_Capture
	{		
		[StructLayout(LayoutKind.Sequential)]
		struct CURSORINFO { public int cbSize; public int flags; public IntPtr hCursor; public POINTAPI ptScreenPos; }
	
		[StructLayout(LayoutKind.Sequential)]
		struct POINTAPI { public int x; public int y; }
	
		[DllImport("user32.dll")]
		static extern bool GetCursorInfo(out CURSORINFO pci);
	
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);
	
		
		public static Bitmap region(Rectangle area, bool cursor = true, PixelFormat pixel_format = PixelFormat.Format32bppRgb) {
			var bmp = new Bitmap(area.Width - 2, area.Height - 2, pixel_format);
			Graphics g = Graphics.FromImage(bmp);
			g.CopyFromScreen(area.X, area.Y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
			
			if (cursor)
			{
				CURSORINFO cursor_info;
				cursor_info.cbSize = Marshal.SizeOf(typeof (CURSORINFO));

				if (GetCursorInfo(out cursor_info))
					if (cursor_info.flags == (int)0x0001)
					{
						var hdc = g.GetHdc();
						DrawIconEx(
                            hdc, cursor_info.ptScreenPos.x - area.X, cursor_info.ptScreenPos.y - area.Y, cursor_info.hCursor,
                            0, 0, 0, IntPtr.Zero, (int)0x0003
                        );
						g.ReleaseHdc();
					}
			}
			
			g.Dispose();
			return bmp;
		}
		
		public static Bitmap screen(bool cursor = true, PixelFormat pixel_format = PixelFormat.Format32bppRgb) {
			return region(Screen.PrimaryScreen.Bounds, cursor, pixel_format);
		}
	}
}
