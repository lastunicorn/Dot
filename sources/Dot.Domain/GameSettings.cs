namespace DustInTheWind.Dot.Domain
{
    public class GameSettings : IGameSettings
    {
        public int LastSavedGame
        {
            get => 0;
            set
            {
                //Settings.Default.LastSaveFileName = value;
                //Settings.Default.Save();
            }
        }
    }
}