using System;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    public class GameTimer
    {
        private DateTime? startTime;
        private TimeSpan totalPlayTime;

        public TimeSpan TotalPlayTime
        {
            get => startTime.HasValue
                ? totalPlayTime + (DateTime.UtcNow - startTime.Value)
                : totalPlayTime;
            set => totalPlayTime = value;
        }

        public GameTimer()
        {
        }

        public GameTimer(TimeSpan initialValue)
        {
            totalPlayTime = initialValue;
        }

        public void Start()
        {
            if (startTime.HasValue)
                return;

            startTime = DateTime.UtcNow;
        }

        public void Stop()
        {
            if (startTime.HasValue)
            {
                totalPlayTime += DateTime.UtcNow - startTime.Value;
                startTime = null;
            }
        }
    }
}