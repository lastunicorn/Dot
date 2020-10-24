using System.Collections.Generic;

namespace DustInTheWind.Dot.Domain.AudioTextModel
{
    public interface IAudioTextEnumerable : IEnumerable<AudioText>
    {
        string MusicAudioFileName { get; set; }
    }
}