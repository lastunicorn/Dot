using System.Collections.Generic;
using DustInTheWind.Dot.Domain;

namespace DustInTheWind.Dot.Application.LoadGame
{
    public interface ILoadGameView
    {
        bool AskToSavePreviousGame();

        GameSlot AskToChooseGameSlot(IEnumerable<GameSlot> gameSlots);

        void DisplaySuccessMessage();
    }
}