using System;
using DustInTheWind.Dot.Presentation.Commands;
using DustInTheWind.Dot.Presentation.Views;

namespace DustInTheWind.Dot.Presentation.Presenters
{
    public class MainMenuPresenter
    {
        private readonly MainMenuView view;

        public MainMenuPresenter(MainMenuView view, ICommandFactory commandFactory)
        {
            if (commandFactory == null) throw new ArgumentNullException(nameof(commandFactory));
            this.view = view ?? throw new ArgumentNullException(nameof(view));

            view.NewGameCommand = commandFactory.Create<NewGameCommand>();
            view.ContinueLastGameCommand = commandFactory.Create<ContinueLastGameCommand>();
            view.ResumeGameCommand = commandFactory.Create<ResumeGameCommand>();
            view.SaveCommand = commandFactory.Create<SaveCommand>();
            view.LoadCommand = commandFactory.Create<LoadCommand>();
            view.SettingsCommand = commandFactory.Create<SettingsCommand>();
            view.HelpCommand = commandFactory.Create<HelpCommand>();
            view.CreditsCommand = commandFactory.Create<CreditsCommand>();
            view.ExitCommand = commandFactory.Create<ExitCommand>();
        }

        public void Display()
        {
            view.Display();
        }
    }
}