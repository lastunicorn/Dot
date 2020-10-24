using System.Collections.Generic;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Application
{
    public interface ISaveGameView
    {
        GameSlot AskToChooseGameSlot(IEnumerable<GameSlot> gameSlots);
    }
}