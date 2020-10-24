using System;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ExitCommand : ICommand
    {
        private readonly IGameApplication gameApplication;

        public event EventHandler CanExecuteChanges;

        public ExitCommand(IGameApplication gameApplication)
        {
            this.gameApplication = gameApplication ?? throw new ArgumentNullException(nameof(gameApplication));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            gameApplication.Close();
        }
    }
}