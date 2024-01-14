using System;

namespace DustInTheWind.Dot.Presentation.ConsoleHelpers
{
    internal class ObjectChipBuilder : IChipBuilder
    {
        public ChipBuilderState State { get; set; }

        public bool Use(char c)
        {
            switch (c)
            {
                case '{':
                    return UseStartChar();

                case '}':
                    return UseStopChar();

                default:
                    return false;
            }
        }

        private bool UseStopChar()
        {
            switch (State)
            {
                case ChipBuilderState.Off:
                    return false;

                case ChipBuilderState.Starting:
                    State = ChipBuilderState.Off;
                    return false;

                case ChipBuilderState.On:
                    State = ChipBuilderState.Ending;
                    return true;

                case ChipBuilderState.Ending:
                    State = ChipBuilderState.Off;
                    return true;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool UseStartChar()
        {
            switch (State)
            {
                case ChipBuilderState.Off:
                    State = ChipBuilderState.Starting;
                    return true;

                case ChipBuilderState.Starting:
                    State = ChipBuilderState.On;
                    return true;

                case ChipBuilderState.On:
                    return true;

                case ChipBuilderState.Ending:
                    State = ChipBuilderState.On;
                    return true;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}