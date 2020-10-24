using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class LookAtActionParameters
    {
        public IObject Object { get; }

        public LookAtActionParameters(IReadOnlyList<object> parameters)
        {
            Object = parameters.Count >= 1
                ? parameters[0] as IObject
                : null;
        }
    }
}