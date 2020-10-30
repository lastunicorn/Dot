using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Domain.GameModel
{
    public interface IGameFactory
    {
        IGameBase CreateNew();

        IGameBase CreateFrom(StorageData gameSlotData);
    }
}