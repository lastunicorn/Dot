using System;

namespace DustInTheWind.Dot.ConsoleHelpers.UIControls
{
    public interface ICommand
    {
        event EventHandler CanExecuteChanges;

        bool CanExecute();

        void Execute();
    }
}