using System;
using DustInTheWind.Dot.GameHosting;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Modules
{
    public class GameModule : IModule
    {
        private readonly IScreenFactory screenFactory;
        private GamePresenter gamePresenter;

        public string Id { get; } = "game";

        public GameModule(IScreenFactory screenFactory)
        {
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
        }

        public string Run()
        {
            gamePresenter = screenFactory.Create<GamePresenter>();
            gamePresenter.Display();

            return null;
        }

        public void RequestExit()
        {
            gamePresenter.RequestExit();
        }
    }
}