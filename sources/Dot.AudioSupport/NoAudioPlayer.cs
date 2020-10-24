namespace DustInTheWind.Dot.AudioSupport
{
    internal class NoAudioPlayer : IAudioPlayer
    {
        public void Play(string url)
        {
        }

        public void PlayRepeat(string url)
        {
        }

        public void SetVolume(int value)
        {
        }

        public void Stop()
        {
        }

        public void WaitToEnd()
        {
        }
    }
}