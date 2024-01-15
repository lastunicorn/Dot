using System.Collections.Generic;

namespace DustInTheWind.Dot.Application.UseCases.LoadGame
{
    public interface ILoadGameView
    {
        bool AskToSavePreviousGame();

        GameSlotId AskToChooseGameSlot(IEnumerable<GameSlotId> gameSlotIds);

        void AnnounceSuccess();
    }
}