using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Application
{
    public class UnknownActionAudioText : AudioText
    {
        private static Random random;
        private static int lastRandomIndex;

        private static readonly List<Tuple<string, string>> Items = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("What is that?", "unknown-action-1.mp3"),
            new Tuple<string, string>("Hmm... And that was supposed to be... what?", "unknown-action-2.mp3"),
            new Tuple<string, string>("I don't understand that.", "unknown-action-3.mp3"),
            new Tuple<string, string>("Are your fingers hitting the wrong keys?", "unknown-action-4.mp3"),
            new Tuple<string, string>("You have to type it again. This time sloooow and clear.", "unknown-action-5.mp3")
        };

        public UnknownActionAudioText()
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