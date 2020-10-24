using System.Collections.Generic;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    public interface IGameSlotRepository
    {
        IEnumerable<GameSlot> GetAll();
        
        void AddOrReplace(GameSlot gameSlot);
    }
}