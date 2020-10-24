using System.Collections;

namespace DustInTheWind.Dot.AdventureGame.ObjectModel
{
    public interface IOpenable : IObject
    {
        bool IsOpened { get; }

        IEnumerable Open();

        IEnumerable Close();
    }
}