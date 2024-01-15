using System;
using Dot.GameHosting;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Application.UseCases.NewGame
{
    public class CreateNewGameUseCase
    {
        private readonly GameRepository gameRepository;
        private readonly IGameFactory gameFactory;
        private readonly ModuleEngine moduleEngine;

        public CreateNewGameUseCase(GameRepository gameRepository, IGameFactory gameFactory, ModuleEngine moduleEngine)
        {
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
            this.moduleEngine = moduleEngine ?? throw new ArgumentNullException(nameof(moduleEngine));
        }

        public void Execute()
        {
            IGame game = gameRepository.Get();
            game?.Close();

            IGame newGame = gameFactory.Create();
            newGame.InitializeNew();
            game = newGame;
            gameRepository.Add(game);

            moduleEngine.RequestToChangeModule("game");
        }
    }
}