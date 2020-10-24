namespace DustInTheWind.Dot.AudioSupport
{
    internal interface IAudioPlayer
    {
        void Play(string url);

        void PlayRepeat(string url);

        void SetVolume(int value);

        void Stop();

        void WaitToEnd();
    }
}