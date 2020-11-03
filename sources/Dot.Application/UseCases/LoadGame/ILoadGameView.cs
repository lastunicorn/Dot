using System.Collections.Generic;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Application.UseCases.LoadGame
{
    public interface ILoadGameView
    {
        bool AskToSavePreviousGame();

        GameSlot AskToChooseGameSlot(IEnumerable<GameSlot> gameSlots);

        void AnnounceSuccess();
    }
}