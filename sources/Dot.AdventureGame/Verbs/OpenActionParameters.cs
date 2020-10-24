using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class OpenActionParameters
    {
        public IOpenable TargetObject { get; }

        public OpenActionParameters(IReadOnlyList<object> parameters)
        {
            TargetObject = parameters.Count >= 1
                ? parameters[0] as IOpenable
                : null;
        }
    }
}