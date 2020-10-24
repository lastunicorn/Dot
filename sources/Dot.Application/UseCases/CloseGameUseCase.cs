using System;
using DustInTheWind.Dot.Domain.DataAccess;
using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Application.UseCases
{
    internal class CloseGameUseCase
    {
        private readonly GameRepository gameRepository;

        public CloseGameUseCase(GameRepository gameRepository)
        {
            this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public void Execute()
        {
            IGameBase game = gameRepository.Get();
            game?.Close();
        }
    }
}