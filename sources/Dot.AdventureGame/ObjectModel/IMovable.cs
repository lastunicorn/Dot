using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IMovable : IObject
    {
        IEnumerable Move();
    }
}