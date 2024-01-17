namespace DustInTheWind.Dot.Domain.GameModel
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