using System.Collections.Generic;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Application.SaveGame
{
    public interface ISaveGameView
    {
        GameSlot AskToChooseGameSlot(IEnumerable<GameSlot> gameSlots);
    }
}