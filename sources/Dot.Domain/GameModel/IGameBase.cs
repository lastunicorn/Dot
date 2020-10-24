using System;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Domain.GameModel
{
    public interface IGameBase
    {
        TimeSpan TotalPlayTime { get; }

        bool IsChanged { get; }

        event EventHandler StateChanged;
        event EventHandler CurrentLocationChanged;
        event EventHandler<GameOpenEventArgs> Opened;
        event EventHandler Closing;
        event EventHandler Closed;

        void Open();

        void Close();

        void Pause();

        void Finish();

        StorageNode Save();

        void Load(StorageNode storageNode);
    }
}