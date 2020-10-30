using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IExportable
    {
        StorageNode Export();

        void Import(StorageNode storageNode);
    }
}