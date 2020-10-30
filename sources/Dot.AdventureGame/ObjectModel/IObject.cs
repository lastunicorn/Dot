using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IObject : IExportable
    {
        string Id { get; }

        string Name { get; }

        string ImagePath { get; }

        IObjectContainer Parent { get; set; }

        bool IsVisible { get; set; }

        IEnumerable LookAt();
    }
}