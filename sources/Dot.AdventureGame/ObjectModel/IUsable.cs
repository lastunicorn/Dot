using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IUsable : IObject
    {
        IEnumerable Use();
    }
}