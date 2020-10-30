using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.GameStorage.Binary
{
    public class GameSlotRepository : IGameSlotRepository
    {
        private readonly string saveDirectory;

        public static string ApplicationDirectory
        {
            get
            {
                Assembly assembly = Assembly.GetEntryAssembly();
                return Path.GetDirectoryName(assembly.Location);
            }
        }

        public GameSlotRepository()
        {
            saveDirectory = Path.Combine(ApplicationDirectory, "save");
        }

        public IEnumerable<GameSlot> GetAll()
        {
            if (!Directory.Exists(saveDirectory))
                return Enumerable.Empty<GameSlot>();

            return Directory.GetFiles(saveDirectory, "save??.sav")
                .Select(x => new GameSlot
                {
                    Id = int.Parse(Path.GetFileNameWithoutExtension(x).Substring(4)),
                    Data = Deserialize(x).ToEntity()
                });
        }

        private static BinaryStorageData Deserialize(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (BinaryStorageData)binaryFormatter.Deserialize(fs);
            }
        }

        public void AddOrReplace(GameSlot gameSlot)
        {
            Directory.CreateDirectory(saveDirectory);

            string fileName = string.Format("save{0:00}.sav", gameSlot.Id);
            string filePath = Path.Combine(saveDirectory, fileName);

            BinaryStorageData binaryStorageData = new BinaryStorageData(gameSlot.Data);
            Serialize(filePath, binaryStorageData);
        }

        private static void Serialize(string fileName, BinaryStorageData storageData)
        {
            using (FileStream fileStream = File.Create(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, storageData);
            }
        }
    }


}