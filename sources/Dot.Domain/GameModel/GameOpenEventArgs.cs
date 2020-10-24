using System;

namespace DustInTheWind.Dot.Domain.GameModel
{
    public class GameOpenEventArgs : EventArgs
    {
        public bool IsNewGame { get; }

        public GameOpenEventArgs(bool isNewGame)
        {
            IsNewGame = isNewGame;
        }
    }
}