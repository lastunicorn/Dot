using System.Collections.Generic;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IObjectContainer : IEnumerable<IObject>
    {
        void AddObject(IObject @object);

        void RemoveObject(IObject @object);

        bool Contains(IObject @object);

        T Find<T>();

        IObject FindVisibleObject(string objectName);
        bool Contains<T>();
        T GetChild<T>();
        void MakeAllChildrenVisible();
    }
}