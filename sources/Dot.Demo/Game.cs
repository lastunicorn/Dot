using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Demo.Objects;

namespace DustInTheWind.Dot.Demo
{
    internal sealed class Game : StandardGame
    {
        public Game()
        {
            Park park = new Park();
            AddLocation(park);

            Crossroads crossroads = new Crossroads();
            AddLocation(crossroads);
        }
    }
}