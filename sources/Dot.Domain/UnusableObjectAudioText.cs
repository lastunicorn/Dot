using System;
using System.Collections.Generic;
using DustInTheWind.Dot.Domain.AudioTextModel;

namespace DustInTheWind.Dot.Domain
{
    public class UnusableObjectAudioText : AudioText
    {
        private static Random random;
        private static int lastRandomIndex;

        private static readonly List<Tuple<string, string>> Items = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("And... how do you want to use it?", "use-unusableobject-1.mp3"),
            new Tuple<string, string>("I don't know a way to use it.", "use-unusableobject-2.mp3"),
            new Tuple<string, string>("Use it... how?", "use-unusableobject-3.mp3")
        };

        public UnusableObjectAudioText()
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