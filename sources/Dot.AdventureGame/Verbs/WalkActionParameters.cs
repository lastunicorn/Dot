using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class WalkActionParameters
    {
        public IExitWay ExitWay { get; }

        public WalkActionParameters(IReadOnlyList<object> parameters)
        {
            ExitWay = parameters.Count >= 1
                ? parameters[0] as IExitWay
                : null;
        }
    }
}