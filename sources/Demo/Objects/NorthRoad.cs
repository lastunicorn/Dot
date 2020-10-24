using System.Collections;
using DustInTheWind.Dot.AdventureGame.ActionResults;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects
{
    internal class NorthRoad : ObjectBase, IExitWay
    {
        public override string Id { get; } = "north-road";

        public override string Name { get; } = "north road";

        public override IEnumerable LookAt()
        {
            return new AudioText
            {
                Text = "The road goes to the north."
            };
        }

        public IEnumerable Exit()
        {
            yield return new ChangeLocationResult
            {
                DestinationId = "crossroads"
            };
        }
    }
}