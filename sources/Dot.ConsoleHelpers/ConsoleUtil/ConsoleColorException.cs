using System;

namespace DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil
{
    [Serializable]
    internal class ConsoleColorException : Exception
    {
        public int ErrorCode { get; private set; }

        public ConsoleColorException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}