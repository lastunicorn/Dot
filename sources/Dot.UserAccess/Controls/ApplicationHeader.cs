using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.UserAccess.Controls
{
    public class ApplicationHeader
    {
        public ConsoleColor TitleColor { get; set; }

        public string ImagePath { get; set; }

        public void Display()
        {
            ApplicationInfo applicationInfo = new ApplicationInfo();
            string productName = applicationInfo.GetProductName();
            string assemblyVersion = applicationInfo.GetAssemblyInformationalVersion();

            CustomConsole.WriteLineEmphasies(productName + " " + assemblyVersion);
            CustomConsole.WriteLine(new string('═', Console.BufferWidth - 1));
            CustomConsole.WriteLine();

            if (ImagePath != null)
                DisplayTitlePicture();
        }

        private void DisplayTitlePicture()
        {
            AsciiImageBox asciiImageBox = new AsciiImageBox
            {
                AsciiPath = ImagePath,
                ForegroundColor = TitleColor
            };
            asciiImageBox.Display();
        }
    }
}