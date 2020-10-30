using System;
using DustInTheWind.Dot.Application.LoadGame;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.ModuleModel;
using DustInTheWind.Dot.Presentation.Presenters;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ContinueLastGameCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly ModuleEngine moduleEngine;
        private readonly IScreenFactory screenFactory;
        private readonly GameRepository gameRepository;
        private readonly IGameSettings gameSettings;

        public event EventHandler CanExecuteChanges;

        public ContinueLastGameCommand(IUseCaseFactory useCaseFactory, ModuleEngine moduleEngine, IScreenFactory screenFactory,
            GameRepository gameRepository, IGameSettings gameSettings)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        }

        public bool CanExecute()
        {
            IGameBase game = gameRepository.Get();
            return game == null && gameSettings.LastSavedGame.HasValue;
        }

        public void Execute()
        {
            LoadGameUseCase useCase = useCaseFactory.Create<LoadGameUseCase>();
            useCase.Execute();

            moduleEngine.RequestToChangeModule("game");

            GamePresenter gamePresenter = screenFactory.Create<GamePresenter>();
            gamePresenter.Display();
        }
    }
}