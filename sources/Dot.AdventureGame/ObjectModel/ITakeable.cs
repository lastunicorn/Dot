using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface ITakeable : IObject
    {
        IEnumerable Take();
    }
}