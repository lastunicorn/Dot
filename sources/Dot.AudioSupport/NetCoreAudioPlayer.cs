using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetCoreAudio;

namespace DustInTheWind.Dot.AudioSupport
{
    internal class NetCoreAudioPlayer : IAudioPlayer, IDisposable
    {
        private readonly Player player;
        private readonly ManualResetEventSlim audioEndEvent;
        private volatile bool stopWasRequested;

        public NetCoreAudioPlayer()
        {
            player = new Player();
            audioEndEvent = new ManualResetEventSlim(false);

            player.PlaybackFinished += (sender, args) =>
            {
                audioEndEvent.Set();
            };
        }

        public void Play(string url)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    //player.settings.setMode("loop", false);
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
            stopWasRequested = false;

            Task.Run(() =>
            {
                while (!stopWasRequested)
                {
                    PlayInternal(url);
                    audioEndEvent.Wait();
                }
            });
        }

        private void PlayInternal(string url)
        {
            audioEndEvent.Reset();

            if (File.Exists(url))
                player.Play(url);
            else
                audioEndEvent.Set();
        }

        public void SetVolume(int value)
        {
            //player.settings.volume = value;
        }

        public void Stop()
        {
            player.Stop();
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