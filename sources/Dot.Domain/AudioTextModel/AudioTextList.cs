using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Dot.Domain.AudioTextModel
{
    public class AudioTextList : List<AudioText>, IAudioTextEnumerable
    {
        private Random random;
        private int lastRandomIndex;

        public string MusicAudioFileName { get; set; }

        public AudioTextList()
        {
            random = new Random();
            lastRandomIndex = -1;
        }

        public AudioTextList(IEnumerable<AudioText> collection)
            : base(collection)
        {
        }

        public AudioText GetOne()
        {
            if (random == null)
                random = new Random();

            while (true)
            {
                int randomIndex = random.Next(Count);

                if (randomIndex == lastRandomIndex)
                    continue;

                lastRandomIndex = randomIndex;
                return this[randomIndex];
            }
        }

        public static AudioTextList FromTexts(IEnumerable<string> texts)
        {
            IEnumerable<AudioText> audioTexts = texts
                .Select(x => new AudioText { Text = x });

            return new AudioTextList(audioTexts);
        }
    }
}