using DustInTheWind.Dot.Ports.ConfigAccess;

namespace DustInTheWind.Dot.ConfigAccess
{
    public class GameSettings : IGameSettings
    {
        public int? LastSavedGame
        {
            get => null;
            set
            {
                //Settings.Default.LastSaveFileName = value;
                //Settings.Default.Save();
            }
        }
    }
}