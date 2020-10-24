using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DustInTheWind.Dot.ConsoleHelpers
{
    public class KeyListener
    {
        private readonly ConsoleKey consoleKey;
        private volatile bool stopRequested;

        public event EventHandler KeyPressed;

        public KeyListener(ConsoleKey consoleKey = ConsoleKey.Escape)
        {
            if (!Enum.IsDefined(typeof(ConsoleKey), consoleKey))
                throw new InvalidEnumArgumentException(nameof(consoleKey), (int)consoleKey, typeof(ConsoleKey));

            this.consoleKey = consoleKey;
        }

        public void StartListen()
        {
            stopRequested = false;
            Task.Run(WaitForEscape);
        }

        private void WaitForEscape()
        {
            // Consume existing keys from buffer.

            while (Console.KeyAvailable)
                Console.ReadKey(true);

            // Wait for new keys.

            while (!stopRequested)
            {
                while (!stopRequested && !Console.KeyAvailable)
                    Thread.Sleep(100);

                if (stopRequested)
                    break;

                if (Console.ReadKey(true).Key == consoleKey)
                    OnKeyPressed();
            }
        }

        public void StopListen()
        {
            stopRequested = true;
        }

        protected virtual void OnKeyPressed()
        {
            KeyPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}