using System;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

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