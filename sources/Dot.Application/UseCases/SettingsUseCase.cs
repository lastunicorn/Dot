using System;

namespace DustInTheWind.Dot.Application.UseCases
{
    public class SettingsUseCase
    {
        private readonly ISettingsView view;

        public SettingsUseCase(ISettingsView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Execute()
        {
            view.DisplayMenu();
        }
    }
}