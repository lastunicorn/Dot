using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ResumeGameCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly GameRepository gameRepository;
        private readonly IScreenFactory screenFactory;

        public event EventHandler CanExecuteChanges;

        public ResumeGameCommand(IUseCaseFactory useCaseFactory, GameRepository gameRepository, IScreenFactory screenFactory)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
        }

        public bool CanExecute()
        {
            IGameBase game = gameRepository.Get();
            return game != null;
        }

        public void Execute()
        {
            GamePresenter gamePresenter = screenFactory.Create<GamePresenter>();
            gamePresenter.Display();
        }

        protected virtual void OnCanExecuteChanges()
        {
            CanExecuteChanges?.Invoke(this, EventArgs.Empty);
        }
    }
}