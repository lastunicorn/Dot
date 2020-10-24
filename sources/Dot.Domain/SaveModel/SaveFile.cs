using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace DustInTheWind.Dot.Domain.SaveModel
{
    public class SaveFile
    {
        public string FileName { get; private set; }

        public SaveData SaveData { get; private set; }

        private SaveFile()
        {
        }

        public void Save(string fileName)
        {
            if (SaveData == null)
                throw new Exception("No data to be saved");

            FileName = fileName;
            SaveData.SaveTime = DateTime.Now;
            SaveData.Version = GetAssemblyVersion();

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
                binaryFormatter.Serialize(fileStream, SaveData);
            }
        }

        private static SaveData Deserialize(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (SaveData)binaryFormatter.Deserialize(fs);
            }
        }

        public static SaveFile Open(string fileName)
        {
            return new SaveFile
            {
                FileName = fileName,
                SaveData = Deserialize(fileName)
            };
        }

        public static SaveFile Empty(string name)
        {
            return new SaveFile
            {
                SaveData = new SaveData
                {
                    Name = name
                }
            };
        }
    }
}