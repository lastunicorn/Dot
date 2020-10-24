using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class ReadActionParameters
    {
        public IReadable Object { get; }

        public ReadActionParameters(IReadOnlyList<object> parameters)
        {
            Object = parameters.Count >= 1
                ? parameters[0] as IReadable
                : null;
        }
    }
}