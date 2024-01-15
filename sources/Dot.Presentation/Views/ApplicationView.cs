using System;
using System.Threading;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.Controls;
using DustInTheWind.Dot.Presentation.WindowsNative;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class ApplicationView : ViewBase
    {
        public ApplicationView(Audio audio)
            : base(audio)
        {
        }

        public void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = CreateApplicationHeader();
            applicationHeader.Display();
        }

        protected virtual ApplicationHeader CreateApplicationHeader()
        {
            return new ApplicationHeader
            {
                TitleColor = DefaultTheme.Instance.TitleColor
            };
        }

        public void ResetConsoleWindow()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();

            CustomConsole.SetSize(80, 1024, 80, 50);

            Display display = new Display();
            ConsoleWindow consoleWindow = new ConsoleWindow();

            int top = 0;
            int left = (display.Width - consoleWindow.Width) / 2;

            consoleWindow.SetPosition(left, top);
        }

        public void DisplayFunctionalityNotImplementedInfo()
        {
            DisplayInfo("Sorry! This functionality is not implemented yet.");
        }

        public void DisplayOperationCanceledInfo()
        {
            DisplayInfo("Operation was canceled.");
        }

        public void DisplayGoodByeMessage()
        {
            DisplayInfo("Bye!");
            Thread.Sleep(1000);
        }

        public void DisplayError(Exception exception)
        {
            CustomConsole.WriteError("Internal error occurred. " + exception);
        }
    }
}