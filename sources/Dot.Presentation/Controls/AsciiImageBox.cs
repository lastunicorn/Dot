using System;
using System.IO;
using System.Threading;
using DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.AsciiModel;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class AsciiImageBox
    {
        private static readonly AsciiResources AsciiResources;

        public string AsciiPath { get; set; }

        public ConsoleColor ForegroundColor { get; set; }

        public int MarginTop { get; set; }

        public int MarginBottom { get; set; }

        static AsciiImageBox()
        {
            AsciiResources = new AsciiResources();
        }

        public void Display()
        {
            Stream asciiStream = AsciiResources.GetAsciiStream(AsciiPath);

            HideCursor(() =>
            {
                WriteTopMargin();

                if (asciiStream != null)
                    WriteContent(asciiStream);

                WriteBottomMargin();
            });
        }

        private static void HideCursor(Action action)
        {
            Console.CursorVisible = false;

            try
            {
                action();
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }

        private void WriteTopMargin()
        {
            for (int i = 0; i < MarginTop; i++)
                CustomConsole.WriteLine();
        }

        private void WriteContent(Stream asciiStream)
        {
            using (StreamReader sr = new StreamReader(asciiStream))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    CustomConsole.WriteLine(line, ForegroundColor);
                    Thread.Sleep(10);
                }
            }
        }

        private void WriteBottomMargin()
        {
            for (int i = 0; i < MarginBottom; i++)
                CustomConsole.WriteLine();
        }
    }
}