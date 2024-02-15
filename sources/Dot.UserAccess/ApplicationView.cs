// Dot
// Copyright (C) 2020-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.Ports.UserAccess;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.Controls;
using DustInTheWind.Dot.Presentation.WindowsNative;
using DustInTheWind.Dot.UserAccess.Controls;

namespace DustInTheWind.Dot.UserAccess;

public class ApplicationView : ViewBase, IPresentation
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

        Display display = new();
        ConsoleWindow consoleWindow = new();

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