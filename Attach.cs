using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WebMCam
{
    class Attach
    {
        public IntPtr Handle = IntPtr.Zero;
        public bool isSet = false;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef handleRef, out Rectangle rect);

        public Attach()
        {
            Handle = GetForegroundWindow();
        }

        public bool Wait()
        {
            IntPtr newHandle = GetForegroundWindow();
            if (Handle != newHandle)
            {
                Handle = newHandle;
                isSet = true;
            }

            return isSet;
        }

        public Rectangle Size()
        {
            if (!isSet)
                return new Rectangle(0, 0, 0, 0);

            Rectangle rct;
            GetWindowRect(new HandleRef(this, Handle), out rct);

            return rct;
        }
    }
}
