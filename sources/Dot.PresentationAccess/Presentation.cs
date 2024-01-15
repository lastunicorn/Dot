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

using DustInTheWind.Dot.Ports.PresentationAccess;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.PresentationAccess;

public class Presentation : IPresentation
{
    private readonly ApplicationView applicationView;

    public Presentation(ApplicationView applicationView)
    {
        this.applicationView = applicationView ?? throw new ArgumentNullException(nameof(applicationView));
    }

    public void DisplayApplicationHeader()
    {
        applicationView.DisplayApplicationHeader();
    }

    public void ResetConsoleWindow()
    {
        applicationView.ResetConsoleWindow();
    }

    public void DisplayFunctionalityNotImplementedInfo()
    {
        applicationView.DisplayFunctionalityNotImplementedInfo();
    }

    public void DisplayOperationCanceledInfo()
    {
        applicationView.DisplayOperationCanceledInfo();
    }

    public void DisplayGoodByeMessage()
    {
        applicationView.DisplayGoodByeMessage();
    }

    public void DisplayError(Exception exception)
    {
        applicationView.DisplayError(exception);
    }
}