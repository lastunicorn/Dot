using System;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class HelpCommand : ICommand
    {
        private readonly HelpView helpView;

        public event EventHandler CanExecuteChanges;

        public HelpCommand(HelpView helpView)
        {
            this.helpView = helpView ?? throw new ArgumentNullException(nameof(helpView));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            helpView.DisplayHelpInformation();
        }
    }
}