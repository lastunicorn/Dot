using System.Runtime.Serialization;
using System.Security.Permissions;
using DustInTheWind.Dot.Ports.GameSavesAccess;

namespace DustInTheWind.Dot.GameSavesAccess.Binary;

[Serializable]
public class BinaryStorageData : BinaryStorageNode
{
    public Version Version { get; set; }

    public DateTime SaveTime { get; set; }

    public BinaryStorageData()
    {
    }

    public BinaryStorageData(StorageData storageData)
        : base(storageData)
    {
        Version = storageData.Version;
        SaveTime = storageData.SaveTime;
    }

    public BinaryStorageData(SerializationInfo info, StreamingContext context)
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

    public new StorageData ToEntity()
    {
        StorageData storageData = new StorageData
        {
            ObjectType = ObjectType,
            Version = Version,
            SaveTime = SaveTime
        };

        foreach ((string key, object value) in this)
        {
            if (value is BinaryStorageNode childBinaryStorageNode)
            {
                StorageNode storageNode = childBinaryStorageNode.ToEntity();
                storageData.Add(key, storageNode);
            }
            else
            {
                storageData.Add(key, value);
            }
        }

        return storageData;
    }
}