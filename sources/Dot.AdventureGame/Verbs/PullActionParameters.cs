using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class PullActionParameters
    {
        public IPullable TargetObject { get; }

        public PullActionParameters(IReadOnlyList<object> parameters)
        {
            TargetObject = parameters.Count >= 1
                ? parameters[0] as IPullable
                : null;
        }
    }
}