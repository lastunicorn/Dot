using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    [Serializable]
    public class StorageData : StorageNode
    {
        public Version Version { get; set; }

        public DateTime SaveTime { get; set; }

        public StorageData()
        {
        }

        public StorageData(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Version = info.GetValue("version", typeof(Version)) as Version;
            SaveTime = info.GetDateTime("save-time");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("version", Version);
            info.AddValue("save-time", SaveTime);
        }
    }
}