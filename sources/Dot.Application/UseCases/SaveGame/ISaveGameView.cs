using System.Collections.Generic;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Application.UseCases.SaveGame
{
    public interface ISaveGameView
    {
        GameSlot SelectGameSlot(IEnumerable<GameSlot> gameSlots);
    }
}