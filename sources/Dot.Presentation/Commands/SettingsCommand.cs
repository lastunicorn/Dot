using System;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class SettingsCommand : ICommand
    {
        private readonly SettingsView view;

        public event EventHandler CanExecuteChanges;

        public SettingsCommand(SettingsView settingsView)
        {
            view = settingsView ?? throw new ArgumentNullException(nameof(settingsView));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            view.DisplayMenu();
        }
    }
}