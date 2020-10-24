using System.Media;

namespace DustInTheWind.Dot.AudioSupport
{
    internal class WavAudioPlayer : IAudioPlayer
    {
        private readonly SoundPlayer player;

        public WavAudioPlayer()
        {
            player = new SoundPlayer();
        }

        public void Play(string url)
        {
            player.SoundLocation = url;
            player.Play();
        }

        public void PlayRepeat(string url)
        {
            player.SoundLocation = url;
            player.PlayLooping();
        }

        public void SetVolume(int value)
        {
            //player.settings.volume = value;
        }

        public void Stop()
        {
            player.Stop();
        }

        public void WaitToEnd()
        {
        }
    }
}