using System;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    [Serializable]
    public class SaveData
    {
        public Version Version { get; set; }

        public string Name { get; set; }

        public DateTime SaveTime { get; set; }

        public StorageNode Data { get; set; }
    }
}