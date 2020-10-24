using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil
{
    public class ScreenColors
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            internal short X;
            internal short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SMALL_RECT
        {
            internal short Left;
            internal short Top;
            internal short Right;
            internal short Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct COLORREF
        {
            internal uint ColorDWORD;

            internal COLORREF(Color color)
            {
                ColorDWORD = color.R + ((uint)color.G << 8) + ((uint)color.B << 16);
            }

            internal COLORREF(uint r, uint g, uint b)
            {
                ColorDWORD = r + (g << 8) + (b << 16);
            }

            internal Color GetColor()
            {
                int red = (int)(0x000000FFU & ColorDWORD);
                int green = (int)(0x0000FF00U & ColorDWORD) >> 8;
                int blue = (int)(0x00FF0000U & ColorDWORD) >> 16;

                return Color.FromArgb(red, green, blue);
            }

            internal void SetColor(Color color)
            {
                uint red = color.R;
                uint green = (uint)color.G << 8;
                uint blue = (uint)color.B << 16;

                ColorDWORD = red + green + blue;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CONSOLE_SCREEN_BUFFER_INFO_EX
        {
            internal int cbSize;
            internal COORD dwSize;
            internal COORD dwCursorPosition;
            internal ushort wAttributes;
            internal SMALL_RECT srWindow;
            internal COORD dwMaximumWindowSize;
            internal ushort wPopupAttributes;
            internal bool bFullscreenSupported;
            internal COLORREF black;
            internal COLORREF darkBlue;
            internal COLORREF darkGreen;
            internal COLORREF darkCyan;
            internal COLORREF darkRed;
            internal COLORREF darkMagenta;
            internal COLORREF darkYellow;
            internal COLORREF gray;
            internal COLORREF darkGray;
            internal COLORREF blue;
            internal COLORREF green;
            internal COLORREF cyan;
            internal COLORREF red;
            internal COLORREF magenta;
            internal COLORREF yellow;
            internal COLORREF white;
        }

        private const int STD_OUTPUT_HANDLE = -11;
        internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

        public static void SetColor(ConsoleColor consoleColor, Color targetColor)
        {
            SetColor(consoleColor, targetColor.R, targetColor.G, targetColor.B);
        }

        public static void SetColor(ConsoleColor color, uint r, uint g, uint b)
        {
            CONSOLE_SCREEN_BUFFER_INFO_EX consoleScreenBuffer = new CONSOLE_SCREEN_BUFFER_INFO_EX();
            consoleScreenBuffer.cbSize = Marshal.SizeOf(consoleScreenBuffer); // 96 = 0x60

            IntPtr consoleOutputHandle = GetStdHandle(STD_OUTPUT_HANDLE);

            if (consoleOutputHandle == INVALID_HANDLE_VALUE)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new ConsoleColorException("Error getting the standard output handle.", errorCode);
            }

            bool isSuccess = GetConsoleScreenBufferInfoEx(consoleOutputHandle, ref consoleScreenBuffer);

            if (!isSuccess)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new ConsoleColorException("Error getting the console screen buffer.", errorCode);
            }

            switch (color)
            {
                case ConsoleColor.Black:
                    consoleScreenBuffer.black = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkBlue:
                    consoleScreenBuffer.darkBlue = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkGreen:
                    consoleScreenBuffer.darkGreen = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkCyan:
                    consoleScreenBuffer.darkCyan = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkRed:
                    consoleScreenBuffer.darkRed = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkMagenta:
                    consoleScreenBuffer.darkMagenta = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkYellow:
                    consoleScreenBuffer.darkYellow = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Gray:
                    consoleScreenBuffer.gray = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkGray:
                    consoleScreenBuffer.darkGray = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Blue:
                    consoleScreenBuffer.blue = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Green:
                    consoleScreenBuffer.green = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Cyan:
                    consoleScreenBuffer.cyan = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Red:
                    consoleScreenBuffer.red = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Magenta:
                    consoleScreenBuffer.magenta = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Yellow:
                    consoleScreenBuffer.yellow = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.White:
                    consoleScreenBuffer.white = new COLORREF(r, g, b);
                    break;
            }

            consoleScreenBuffer.srWindow.Bottom++;
            consoleScreenBuffer.srWindow.Right++;

            isSuccess = SetConsoleScreenBufferInfoEx(consoleOutputHandle, ref consoleScreenBuffer);

            if (!isSuccess)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new ConsoleColorException("Error setting the console screen buffer with new values.", errorCode);
            }
        }
    }
}