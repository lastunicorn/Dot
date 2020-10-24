using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class PushActionParameters
    {
        public IPushable TargetObject { get; }

        public PushActionParameters(IReadOnlyList<object> parameters)
        {
            TargetObject = parameters.Count >= 1
                ? parameters[0] as IPushable
                : null;
        }
    }
}