using System;
using Dot.GameHosting;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Modules
{
    public class MenuModule : IModule
    {
        private readonly IScreenFactory screenFactory;
        private volatile bool closeWasRequested;

        public string Id { get; } = "main-menu";

        public MenuModule(IScreenFactory screenFactory)
        {
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
        }

        public string Run()
        {
            closeWasRequested = false;

            while (!closeWasRequested)
            {
                try
                {
                    MainMenuPresenter presenter = screenFactory.Create<MainMenuPresenter>();
                    presenter.Display();
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    CustomConsole.WriteError(ex);
                }
            }

            return null;
        }

        public void RequestExit()
        {
            closeWasRequested = true;
        }
    }
}