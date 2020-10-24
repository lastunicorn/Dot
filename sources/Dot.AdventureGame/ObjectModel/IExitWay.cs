using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IExitWay : IObject
    {
        IEnumerable Exit();
    }
}