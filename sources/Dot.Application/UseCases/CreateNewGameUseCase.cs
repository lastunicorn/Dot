using System;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;
using DustInTheWind.Dot.Domain.ModuleModel;

namespace DustInTheWind.Dot.Application.UseCases
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
            IGameBase game = gameRepository.Get();
            game?.Close();

            game = gameFactory.Create();
            gameRepository.Add(game);
        }
    }
}