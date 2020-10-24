using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Domain
{
    public class IncompatibleObjectAudioText : AudioText
    {
        private static Random random;
        private static int lastRandomIndex;

        private static readonly List<Tuple<string, string>> Items = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("You cannot use them in that way.", "use-incompatibleobjects-1.mp3"),
            new Tuple<string, string>("And.. how do you think I can use those two together?", "use-incompatibleobjects-2.mp3"),
            new Tuple<string, string>("Nope.", "use-incompatibleobjects-3.mp3"),
            new Tuple<string, string>("I was considering your suggestion, but no.", "use-incompatibleobjects-4.mp3")
        };

        public IncompatibleObjectAudioText()
        {
            (Text, AudioFileName) = GetOneItem();
        }

        public Tuple<string, string> GetOneItem()
        {
            if (random == null)
                random = new Random();

            while (true)
            {
                int randomIndex = random.Next(Items.Count);

                if (randomIndex == lastRandomIndex)
                    continue;

                lastRandomIndex = randomIndex;
                return Items[randomIndex];
            }
        }
    }
}