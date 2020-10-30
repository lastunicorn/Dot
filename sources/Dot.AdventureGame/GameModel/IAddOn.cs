using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public interface IAddOn
    {
        string Id { get; }

        GameBase Game { get; set; }

        void Start();

        void Stop();

        StorageNode Export();

        void Import(StorageNode storageNode);
    }
}