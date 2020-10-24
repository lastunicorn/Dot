using System;
using System.IO;

namespace DustInTheWind.Dot.AudioSupport
{
    internal sealed class TemporaryFile : IDisposable
    {
        private readonly string extension;

        public string FilePath { get; set; }

        public TemporaryFile(string originalFilePath, string extension)
        {
            this.extension = extension;

            if (File.Exists(originalFilePath))
            {
                using (FileStream originalFileStream = File.OpenRead(originalFilePath))
                {
                    GenerateFilePath();
                    FillInFile(originalFileStream);
                }
            }
        }

        public TemporaryFile(Stream originalContent, string extension)
        {
            this.extension = extension;

            GenerateFilePath();
            FillInFile(originalContent);
        }

        private void GenerateFilePath()
        {
            string tempDirectoryPath = Path.GetTempPath();

            Guid guid = Guid.NewGuid();
            string tempFileName = guid.ToString("N") + extension;

            FilePath = Path.Combine(tempDirectoryPath, tempFileName);
        }

        private void FillInFile(Stream content)
        {
            if (content != null && content.CanRead)
            {
                using (FileStream destination = File.Create(FilePath))
                {
                    byte[] buffer = new byte[10240];
                    int actualReadCount;

                    while ((actualReadCount = content.Read(buffer, 0, buffer.Length)) > 0)
                        destination.Write(buffer, 0, actualReadCount);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                if (File.Exists(FilePath))
                    File.Delete(FilePath);
            }
            catch { }
        }
    }
}