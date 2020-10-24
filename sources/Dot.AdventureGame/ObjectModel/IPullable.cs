using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IPullable : IObject
    {
        IEnumerable Pull();
    }
}