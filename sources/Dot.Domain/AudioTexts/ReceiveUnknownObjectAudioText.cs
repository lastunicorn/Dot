using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Domain.AudioTexts
{
    public class ReceiveUnknownObjectAudioText : AudioText
    {
        private static Random random;
        private static int lastRandomIndex;

        private static readonly List<Tuple<string, string>> Items = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("I don't know what to do with those two.", "receive-unknownobject-1.mp3"),
            new Tuple<string, string>("No idea how you want to use those two together.", "receive-unknownobject-2.mp3"),
            new Tuple<string, string>("No idea how to use those together.", "receive-unknownobject-3.mp3"),
        };

        public ReceiveUnknownObjectAudioText()
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