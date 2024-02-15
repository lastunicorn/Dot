namespace DustInTheWind.Dot.Ports.GameSavesAccess;

public interface IGameSlotRepository
{
    IEnumerable<GameSlot> GetAll();

    void AddOrReplace(GameSlot gameSlot);
}