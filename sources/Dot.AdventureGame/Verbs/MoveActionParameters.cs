using System.Collections.Generic;
using DustInTheWind.Dot.AdventureGame.ObjectModel;

namespace DustInTheWind.Dot.AdventureGame.Verbs
{
    internal class MoveActionParameters
    {
        public bool TargetObjectIsSpecified { get; }

        public bool TargetObjectIsRecognized { get; }

        public bool TargetObjectIsMovable { get; }

        public IMovable TargetObject { get; }

        public MoveActionParameters(IReadOnlyList<object> parameters)
        {
            if (parameters?.Count >= 1)
            {
                TargetObjectIsSpecified = true;

                if (parameters[0] is IMovable movableObject)
                {
                    TargetObjectIsRecognized = true;
                    TargetObjectIsMovable = true;
                    TargetObject = movableObject;
                }
                else if (parameters[0] is IObject)
                {
                    TargetObjectIsRecognized = true;
                    TargetObjectIsMovable = false;
                    TargetObject = null;
                }
                else
                {
                    TargetObjectIsRecognized = false;
                    TargetObjectIsMovable = false;
                    TargetObject = null;
                }
            }
        }
    }
}