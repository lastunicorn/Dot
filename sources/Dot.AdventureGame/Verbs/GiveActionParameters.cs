using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class GiveActionParameters
    {
        public IObject TargetObject { get; }

        public IReceiver Receiver { get; }

        public GiveActionParameters(IReadOnlyList<object> parameters)
        {
            TargetObject = parameters.Count >= 1
                ? parameters[0] as IObject
                : null;

            Receiver = parameters.Count >= 2
                ? parameters[1] as IReceiver
                : null;
        }
    }
}