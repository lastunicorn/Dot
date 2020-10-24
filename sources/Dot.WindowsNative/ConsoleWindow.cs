using System;

namespace DustInTheWind.Dot.WindowsNative
{
    public class ConsoleWindow
    {
        private readonly IntPtr handle;
        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ConsoleWindow()
        {
            handle = NativeMethods.GetConsoleWindow();

            if (NativeMethods.GetWindowRect(handle, out NativeMethods.RECT rect))
            {
                Left = rect.Left;
                Top = rect.Left;
                Width = rect.Right - rect.Left + 1;
                Height = rect.Bottom - rect.Top + 1;
            }
        }

        public void SetPosition(int left, int top)
        {
            const uint flags = NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOACTIVATE;
            NativeMethods.SetWindowPos(handle, IntPtr.Zero, left, top, 0, 0, flags);
        }
    }
}