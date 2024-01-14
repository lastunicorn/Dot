using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.Domain.GameModel
{
    public interface IGameFactory
    {
        IGame CreateNew();

        IGame CreateFrom(StorageData gameSlotData);
    }
}