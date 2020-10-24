using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IReceiver : IObject
    {
        IEnumerable Receive(IObject receivedObject);
    }
}