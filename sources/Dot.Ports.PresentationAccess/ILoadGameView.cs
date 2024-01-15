namespace DustInTheWind.Dot.Ports.PresentationAccess
{
    public interface ILoadGameView
    {
        bool AskToSavePreviousGame();

        GameSlotId AskToChooseGameSlot(IEnumerable<GameSlotId> gameSlotIds);

        void AnnounceSuccess();
    }
}