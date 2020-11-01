using System.Collections.Generic;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Application.LoadGame
{
    public interface ILoadGameView
    {
        bool AskToSavePreviousGame();

        GameSlot AskToChooseGameSlot(IEnumerable<GameSlot> gameSlots);

        void AnnounceSuccess();
    }
}