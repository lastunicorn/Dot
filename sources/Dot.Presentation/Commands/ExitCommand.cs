using System;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.GameHosting;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ExitCommand : ICommand
    {
        private readonly IModuleHost moduleHost;

        public event EventHandler CanExecuteChanges;

        public ExitCommand(IModuleHost moduleHost)
        {
            this.moduleHost = moduleHost ?? throw new ArgumentNullException(nameof(moduleHost));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            moduleHost.Close();
        }
    }
}