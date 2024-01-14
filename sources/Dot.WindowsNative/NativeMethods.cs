using System;
using System.Runtime.InteropServices;

namespace DustInTheWind.Dot.Presentation.WindowsNative
{
    internal static class NativeMethods
    {
        public const int MONITOR_DEFAULTTOPRIMERTY = 0x00000001;
        public const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        public const uint SWP_NOSIZE = 0x01;
        public const uint SWP_NOZORDER = 0x4;
        public const uint SWP_NOACTIVATE = 0x10;

        [DllImport("kernel32")]
        public static extern IntPtr GetConsoleWindow();


        [DllImport("user32")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);


        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);


        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, NativeMonitorInfo lpmi);


        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct NativeRectangle
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;


            public NativeRectangle(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public sealed class NativeMonitorInfo
        {
            public int Size = Marshal.SizeOf(typeof(NativeMonitorInfo));
            public NativeRectangle Monitor;
            public NativeRectangle Work;
            public int Flags;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; // x position of upper-left corner
            public int Top; // y position of upper-left corner
            public int Right; // x position of lower-right corner
            public int Bottom; // y position of lower-right corner
        }
    }
}