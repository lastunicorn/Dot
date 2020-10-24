using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DustInTheWind.Dot.AudioSupport
{
    public class Audio
    {
        private readonly string audioDirectory;

        private readonly Dictionary<AudioChannelType, IAudioPlayer> players = new Dictionary<AudioChannelType, IAudioPlayer>();

        public Assembly AudioAssembly { get; set; }

        public string AudioNamespace { get; set; }

        public Audio()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            string applicationDirectory = assembly != null
                ? Path.GetDirectoryName(assembly.Location)
                : Environment.CurrentDirectory;

            audioDirectory = applicationDirectory != null
                ? Path.Combine(applicationDirectory, "audio")
                : string.Empty;
        }

        private void EnsureChannelExists(AudioChannelType channel)
        {
            if (players.ContainsKey(channel))
                return;

            WindowsAudioPlayer player = new WindowsAudioPlayer();
            players.Add(channel, player);
        }

        public void Play(string audioFileName, AudioChannelType channel)
        {
            EnsureChannelExists(channel);

            string audioResourceName = AudioNamespace + "." + audioFileName;
            using (Stream stream = AudioAssembly.GetManifestResourceStream(audioResourceName))
            using (TemporaryFile temporaryFile = new TemporaryFile(stream, ".mp3"))
            {
                players[channel].Play(temporaryFile.FilePath);
            }
        }

        public void PlayRepeat(string audioFileName, AudioChannelType channel)
        {
            EnsureChannelExists(channel);

            string audioResourceName = AudioNamespace + "." + audioFileName;
            using (Stream stream = AudioAssembly.GetManifestResourceStream(audioResourceName))
            using (TemporaryFile temporaryFile = new TemporaryFile(stream, ".mp3"))
            {
                players[channel].PlayRepeat(temporaryFile.FilePath);
            }
        }

        public void SetVolume(int value, AudioChannelType channel)
        {
            EnsureChannelExists(channel);
            players[channel].SetVolume(value);
        }

        public void StopAll()
        {
            foreach (IAudioPlayer player in players.Values)
                player.Stop();
        }

        public void Stop(AudioChannelType channel)
        {
            if (!players.ContainsKey(channel))
                return;

            players[channel].Stop();
        }

        public void WaitChannel(AudioChannelType channel)
        {
            players[channel].WaitToEnd();
        }
    }
}