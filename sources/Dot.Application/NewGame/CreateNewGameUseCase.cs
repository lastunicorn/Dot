using System;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Application.NewGame
{
    public class CreateNewGameUseCase
    {
        private readonly GameRepository gameRepository;
        private readonly IGameFactory gameFactory;

        public CreateNewGameUseCase(GameRepository gameRepository, IGameFactory gameFactory)
        {
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
        }

        public void Execute()
        {
            IGameBase game = gameRepository.Get();
            game?.Close();

            game = gameFactory.CreateNew();
            gameRepository.Add(game);
        }
    }
}