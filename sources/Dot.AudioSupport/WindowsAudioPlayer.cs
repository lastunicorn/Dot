using System;
using System.IO;
using System.Threading;
using WMPLib;

namespace DustInTheWind.Dot.AudioSupport
{
    internal class WindowsAudioPlayer : IAudioPlayer, IDisposable
    {
        private readonly WindowsMediaPlayer player;
        private readonly ManualResetEventSlim audioEndEvent;

        public WindowsAudioPlayer()
        {
            player = new WindowsMediaPlayer();
            audioEndEvent = new ManualResetEventSlim(false);

            player.PlayStateChange += newState =>
            {
                if (newState == (int)WMPPlayState.wmppsMediaEnded || newState == (int)WMPPlayState.wmppsStopped)
                    audioEndEvent.Set();
            };
        }

        public void Play(string url)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    player.settings.setMode("loop", false);
                    PlayInternal(url);
                    break;
                }
                catch
                {
                    if (i == 2)
                        throw;
                }
            }
        }

        public void PlayRepeat(string url)
        {
            player.settings.setMode("loop", true);
            PlayInternal(url);
        }

        private void PlayInternal(string url)
        {
            audioEndEvent.Reset();

            if (File.Exists(url))
                player.URL = url;
            else
                audioEndEvent.Set();
        }

        public void SetVolume(int value)
        {
            player.settings.volume = value;
        }

        public void Stop()
        {
            player.controls.stop();
            audioEndEvent.Set();
        }

        public void WaitToEnd()
        {
            audioEndEvent.Wait();
        }

        public void Dispose()
        {
            audioEndEvent.Dispose();
        }
    }
}