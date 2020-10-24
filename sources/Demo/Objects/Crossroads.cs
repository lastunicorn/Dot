using System.Collections;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects
{
    internal class Crossroads : LocationBase
    {
        public override string Id { get; } = "crossroads";

        public override string Name { get; } = "Crossroads";

        public override IEnumerable LookAt()
        {
            yield return CreateDescriptionStory(new AudioText
            {
                Text = "The cars are making allot of noise."
            });

            MakeAllChildrenVisible();
        }

        public override AudioText ResumeDescription { get; } = new AudioText
        {
            Text = "You are back in the city, next to the crossroads. The cars are making allot of noise."
        };
    }
}