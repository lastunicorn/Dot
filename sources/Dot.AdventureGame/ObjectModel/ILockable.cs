using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface ILockable : IObject
    {
        bool IsLocked { get; }

        IEnumerable Lock();

        IEnumerable Unlock();
    }
}