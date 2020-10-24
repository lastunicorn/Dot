using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    public class SaveFile
    {
        public string FileName { get; private set; }

        public StorageData StorageData { get; private set; }

        private SaveFile()
        {
        }

        public void Save(string fileName)
        {
            if (StorageData == null)
                throw new Exception("No data to be saved");

            FileName = fileName;
            StorageData.SaveTime = DateTime.Now;
            StorageData.Version = GetAssemblyVersion();

            Serialize(fileName);
        }

        private static Version GetAssemblyVersion()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();

            return assemblyName.Version;
        }

        private void Serialize(string fileName)
        {
            using (FileStream fileStream = File.Create(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, StorageData);
            }
        }

        private static StorageData Deserialize(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (StorageData)binaryFormatter.Deserialize(fs);
            }
        }

        public static SaveFile Open(string fileName)
        {
            return new SaveFile
            {
                FileName = fileName,
                StorageData = Deserialize(fileName)
            };
        }

        public static SaveFile Empty(string name)
        {
            return new SaveFile
            {
                StorageData = new StorageData()
            };
        }
    }
}