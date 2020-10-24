using DustInTheWind.Dot.AdventureGame.GameModel;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.LocationModel
{
    public interface IAddOn
    {
        string Id { get; }

        GameBase Game { get; set; }

        void Start();

        void Stop();

        StorageNode Export();

        void Load(StorageNode storageNode);
    }
}