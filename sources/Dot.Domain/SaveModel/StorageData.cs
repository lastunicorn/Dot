using System;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    public class StorageData : StorageNode
    {
        public Version Version { get; set; }

        public DateTime SaveTime { get; set; }
    }
}