using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Demo.Objects;

namespace DustInTheWind.Dot.Demo
{
    internal sealed class DemoGame : StandardGame
    {
        public override void InitializeNew()
        {
            ParkLocation parkLocation = new ParkLocation();
            parkLocation.InitializeNew();
            AddLocation(parkLocation);

            CrossroadsLocation crossroadsLocation = new CrossroadsLocation();
            crossroadsLocation.InitializeNew();
            AddLocation(crossroadsLocation);
        }
    }
}