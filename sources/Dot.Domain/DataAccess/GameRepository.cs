using DustInTheWind.Dot.Domain.GameModel;

namespace DustInTheWind.Dot.Domain.DataAccess
{
    public class GameRepository
    {
        private IGame game;

        public IGame Get()
        {
            return game;
        }

        public void Add(IGame game)
        {
            this.game = game;
        }
    }
}