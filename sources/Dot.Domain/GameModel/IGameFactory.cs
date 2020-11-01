using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Domain.GameModel
{
    public interface IGameFactory
    {
        IGame CreateNew();

        IGame CreateFrom(StorageData gameSlotData);
    }
}