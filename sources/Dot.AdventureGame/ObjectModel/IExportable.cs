using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IExportable
    {
        StorageNode Export();

        void Import(StorageNode storageNode);
    }
}