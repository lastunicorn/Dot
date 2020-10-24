using System;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    [Serializable]
    public class StorageData : StorageDataNode
    {
        public Version Version { get; set; }

        public DateTime SaveTime { get; set; }
    }
}