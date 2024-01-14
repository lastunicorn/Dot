namespace DustInTheWind.Dot.Ports.GameSavesAccess;

public class StorageData : StorageNode
{
    public Version Version { get; set; }

    public DateTime SaveTime { get; set; }
}