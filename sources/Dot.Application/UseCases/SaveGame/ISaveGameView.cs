using System.Collections.Generic;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.Application.UseCases.SaveGame
{
    public interface ISaveGameView
    {
        GameSlot SelectGameSlot(IEnumerable<GameSlot> gameSlots);
    }
}