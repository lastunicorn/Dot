using System.Collections.Generic;

namespace DustInTheWind.Dot.Domain.AudioTextModel
{
    public interface IAudioText : IEnumerable<IAudioText>
    {
        string MusicAudioFileName { get; set; }
    }
}