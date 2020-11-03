using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.Dot.Domain.AudioTextModel
{
    public class AudioText : IAudioText
    {
        public string Text { get; set; }

        public string AudioFileName { get; set; }

        public string MusicAudioFileName { get; set; }

        public static AudioText Empty => new AudioText { Text = string.Empty };

        public IEnumerator<IAudioText> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static AudioText FromText(string text)
        {
            return new AudioText { Text = text };
        }
    }
}