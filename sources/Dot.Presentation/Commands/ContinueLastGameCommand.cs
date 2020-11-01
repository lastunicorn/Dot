using System;
using DustInTheWind.Dot.Application;
using DustInTheWind.Dot.Application.LoadGame;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Presentation.Commands
{
    internal class ContinueLastGameCommand : ICommand
    {
        private readonly IUseCaseFactory useCaseFactory;
        private readonly GameRepository gameRepository;
        private readonly IGameSettings gameSettings;

        public event EventHandler CanExecuteChanges;

        public ContinueLastGameCommand(IUseCaseFactory useCaseFactory, GameRepository gameRepository, IGameSettings gameSettings)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        }

        public bool CanExecute()
        {
            IGame game = gameRepository.Get();
            return game == null && gameSettings.LastSavedGame.HasValue;
        }

        public void Execute()
        {
            LoadGameUseCase useCase = useCaseFactory.Create<LoadGameUseCase>();
            useCase.Execute();
        }
    }
}