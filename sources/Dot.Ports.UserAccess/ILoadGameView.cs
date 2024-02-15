namespace DustInTheWind.Dot.Ports.UserAccess
{
    public interface ILoadGameView
    {
        bool AskToSavePreviousGame();

        GameSlotId AskToChooseGameSlot(IEnumerable<GameSlotId> gameSlotIds);

        void AnnounceSuccess();
    }
}