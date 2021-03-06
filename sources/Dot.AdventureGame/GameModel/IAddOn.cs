using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public interface IAddOn
    {
        string Id { get; }

        Game Game { get; set; }

        void Start();

        void Stop();

        StorageNode Export();

        void Import(StorageNode storageNode);
    }
}