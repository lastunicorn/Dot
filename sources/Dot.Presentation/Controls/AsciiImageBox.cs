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

            Console.CursorVisible = false;

            try
            {
                for (int i = 0; i < MarginTop; i++)
                    CustomConsole.WriteLine();

                if (asciiStream != null)
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

                for (int i = 0; i < MarginBottom; i++)
                    CustomConsole.WriteLine();
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }
    }
}