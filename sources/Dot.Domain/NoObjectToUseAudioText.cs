using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Domain
{
    public class NoObjectToUseAudioText : AudioText
    {
        private static Random random;
        private static int lastRandomIndex;

        private static readonly List<Tuple<string, string>> Items = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Use what?", "use-noobject-1.mp3"),
            new Tuple<string, string>("Are you going to enlighten me about the objects to use?", "use-noobject-2.mp3"),
            new Tuple<string, string>("You know... you forgot to specified the object to use.", "use-noobject-3.mp3")
        };

        public NoObjectToUseAudioText()
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