using System;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil
{
    public static class CustomConsole
    {
        public static void SetSize(int bufferWidth, int bufferHeight, int windowWidth, int windowHeight)
        {
            int desiredWindowWidth = Math.Min(windowWidth, Console.LargestWindowWidth);

            if (Console.WindowWidth > desiredWindowWidth)
            {
                Console.WindowWidth = desiredWindowWidth;
                Console.BufferWidth = bufferWidth;
            }
            else if (Console.WindowWidth < desiredWindowWidth)
            {
                Console.BufferWidth = bufferWidth;
                Console.WindowWidth = desiredWindowWidth;
            }

            int desiredWindowHeight = Math.Min(windowHeight, Console.LargestWindowHeight);

            if (Console.WindowHeight > desiredWindowHeight)
            {
                Console.WindowHeight = desiredWindowHeight;
                Console.BufferHeight = bufferHeight;
            }
            else if (Console.WindowHeight < desiredWindowHeight)
            {
                Console.BufferHeight = bufferHeight;
                Console.WindowHeight = desiredWindowHeight;
            }
        }

        public static void WriteLineEmphasies(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteEmphasies(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteError(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteError(Exception ex)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLine()
        {
            Console.WriteLine();
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        public static void Write(string text)
        {
            Console.Write(text);
        }

        public static void Write(char c, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(c);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public static void Write(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void Write(string text, ConsoleColor color, HorizontalAlign horizontalAlign)
        {
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlign);

            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLine(string text, ConsoleColor color, HorizontalAlign horizontalAlign)
        {
            Console.CursorLeft = CalculateStartPosition(text, horizontalAlign);

            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        private static int CalculateStartPosition(string text, HorizontalAlign horizontalAlign)
        {
            int startPosition;

            switch (horizontalAlign)
            {
                case HorizontalAlign.Left:
                    startPosition = 0;
                    break;

                case HorizontalAlign.Center:
                    int bufferCenter = Console.BufferWidth / 2 - 2;
                    startPosition = bufferCenter - text.Length / 2;
                    break;

                case HorizontalAlign.Right:
                    int bufferRight = Console.BufferWidth - 1;
                    startPosition = bufferRight - text.Length;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("horizontalAlign", horizontalAlign, null);
            }

            return startPosition;
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static string ReadAction()
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            try
            {
                return ReadLine();
            }
            finally
            {
                Console.ForegroundColor = oldColor;
            }
        }

        public static ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public static ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }
    }
}