using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IPushable : IObject
    {
        IEnumerable Push();
    }
}