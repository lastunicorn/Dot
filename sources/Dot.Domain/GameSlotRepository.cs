using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using DustInTheWind.Dot.Domain.SaveModel;

namespace DustInTheWind.Dot.Domain
{
    public class GameSlotRepository
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
                    Data = Deserialize(x)
                });
        }

        private static SaveData Deserialize(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (SaveData)binaryFormatter.Deserialize(fs);
            }
        }

        public void AddOrReplace(GameSlot gameSlot)
        {
            Directory.CreateDirectory(saveDirectory);

            string fileName = string.Format("save{0:00}.sav", gameSlot.Id);
            string filePath = Path.Combine(saveDirectory, fileName);

            Serialize(filePath, gameSlot.Data);
        }

        private static void Serialize(string fileName, SaveData saveData)
        {
            using (FileStream fileStream = File.Create(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, saveData);
            }
        }
    }
}