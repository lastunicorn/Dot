using System;

namespace DustInTheWind.Dot.WindowsNative
{
    public class Display
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Display()
        {
            IntPtr consoleHandle = NativeMethods.GetConsoleWindow();
            IntPtr monitorHandle = NativeMethods.MonitorFromWindow(consoleHandle, NativeMethods.MONITOR_DEFAULTTONEAREST);


            if (monitorHandle != IntPtr.Zero)
            {
                NativeMethods.NativeMonitorInfo monitorInfo = new NativeMethods.NativeMonitorInfo();
                NativeMethods.GetMonitorInfo(monitorHandle, monitorInfo);

                Left = monitorInfo.Monitor.Left;
                Top = monitorInfo.Monitor.Top;
                Width = (monitorInfo.Monitor.Right - monitorInfo.Monitor.Left);
                Height = (monitorInfo.Monitor.Bottom - monitorInfo.Monitor.Top);
            }
        }
    }
}