using System.Collections;
using DustInTheWind.Dot.AdventureGame.ObjectModel;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Demo.Objects
{
    internal class Oak : ObjectBase
    {
        public override string Id { get; } = "oak";

        public override string Name { get; } = "oak";
        
        public override IEnumerable LookAt()
        {
            return new AudioText
            {
                Text = "A tall oak."
            };
        }
    }
}