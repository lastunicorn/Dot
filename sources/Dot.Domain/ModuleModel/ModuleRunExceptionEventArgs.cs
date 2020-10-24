using System;

namespace DustInTheWind.Dot.Domain.ModuleModel
{
    public class ModuleRunExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public string NextModule { get; set; }

        public ModuleRunExceptionEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }
}