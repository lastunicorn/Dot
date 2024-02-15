namespace DustInTheWind.Dot.Ports.ConfigAccess
{
    public interface IGameSettings
    {
        int? LastSavedGame { get; set; }
    }
}