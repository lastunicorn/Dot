using System;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class Separator
    {
        public ConsoleColor ForegroundColor { get; set; }

        public void Display()
        {
            AsciiImageBox asciiImageBox = new AsciiImageBox
            {
                AsciiPath = "DustInTheWind.Dot.Presentation.Ascii.separator.ascii",
                ForegroundColor = ForegroundColor,
                MarginTop = 2,
                MarginBottom = 2
            };
            asciiImageBox.Display();
        }
    }
}