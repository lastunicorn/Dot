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

namespace Dot.GameHosting;

public class StandardModuleHost : IModuleHost
{
    private volatile bool closeWasRequested;

    private readonly IPresentation presentation;
    private readonly ModuleEngine moduleEngine;

    public StandardModuleHost(IPresentation presentation, ModuleEngine moduleEngine)
    {
        this.presentation = presentation ?? throw new ArgumentNullException(nameof(presentation));
        this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));

        moduleEngine.ModuleRunException += HandleModuleRunException;
    }

    protected void HandleModuleRunException(object sender, ModuleRunExceptionEventArgs e)
    {
        switch (e.Exception)
        {
            case NotImplementedException:
                presentation.DisplayFunctionalityNotImplementedInfo();
                e.NextModule = "main-menu";
                break;

            case OperationCanceledException:
                presentation.DisplayOperationCanceledInfo();
                e.NextModule = "main-menu";
                break;

            default:
                presentation.DisplayError(e.Exception);
                e.NextModule = null;
                break;
        }
    }

    public void Run()
    {
        presentation.ResetConsoleWindow();
        presentation.DisplayApplicationHeader();

        closeWasRequested = false;

        while (!closeWasRequested)
        {
            try
            {
                moduleEngine.Run();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                presentation.DisplayError(ex);
            }
        }

        presentation.DisplayGoodByeMessage();
    }

    public void Close()
    {
        closeWasRequested = true;
        moduleEngine.Close();
    }
}