using System.Collections;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IObject
    {
        string Id { get; }

        string Name { get; }

        string ImagePath { get; }

        IObjectContainer Parent { get; set; }

        bool IsVisible { get; set; }

        IEnumerable LookAt();

        StorageDataNode Export();

        void Import(StorageDataNode storageDataNode);
    }
}