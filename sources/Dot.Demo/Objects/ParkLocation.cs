using System.Collections;
using DustInTheWind.Dot.AdventureGame.LocationModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects
{
    internal class ParkLocation : LocationBase
    {
        public override string Id { get; } = "park";

        public override string Name { get; } = "Park";

        public override void InitializeNew()
        {
            Oak oak = new Oak();
            AddObject(oak);

            NorthRoad northRoad = new NorthRoad();
            AddObject(northRoad);
        }
        
        public override IEnumerable LookAt()
        {
            yield return CreateDescriptionStory(new AudioTextList
            {
                new AudioText
                {
                    Text = "You are standing in the middle of a beautiful park. " +
                           "Next to you there is a tall {{oak}}. " +
                           "You feel that you know that {{oak}} from a distant past, " +
                           "but you cannot remember anything else."
                },
                new AudioText
                {
                    Text = "Further, in the distance, to the north ({{north road}}), the city sounds can be heard."
                }
            });

            MakeAllChildrenVisible();
        }

        public override AudioText ResumeDescription { get; } = new AudioText
        {
            Text = "You are back in the middle of the park, next to the tall oak."
        };
    }
}