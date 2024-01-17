using System;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.GameHosting;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ExitCommand : ICommand
    {
        private readonly ModuleHost moduleHost;

        public event EventHandler CanExecuteChanges;

        public ExitCommand(ModuleHost moduleHost)
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