using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Domain.DataAccess
{
    public class GameRepository
    {
        private IGameBase game;

        public IGameBase Get()
        {
            return game;
        }

        public void Add(IGameBase game)
        {
            this.game = game;
        }
    }
}