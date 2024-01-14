using System;

namespace DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls
{
    public interface ICommand
    {
        event EventHandler CanExecuteChanges;

        bool CanExecute();

        void Execute();
    }
}