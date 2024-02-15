using System.Reflection;

namespace DustInTheWind.Dot.Presentation.Controls.AsciiModel
{
    public class AsciiResources
    {
        private readonly Assembly[] assemblies;

        public AsciiResources()
        {
            assemblies = LoadAssemblies();
        }

        private static Assembly[] LoadAssemblies()
        {
            string applicationDirectoryPath = GetApplicationDirectoryPath();

            if (!Directory.Exists(applicationDirectoryPath))
                return new Assembly[0];

            string[] fileNames = Directory.GetFiles(applicationDirectoryPath);

            return fileNames
                .Select(x =>
                {
                    try
                    {
                        return Assembly.LoadFrom(x);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(x => x != null)
                .ToArray();
        }

        private static string GetApplicationDirectoryPath()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();

            return entryAssembly == null
                ? Environment.CurrentDirectory
                : Path.GetDirectoryName(entryAssembly.Location);
        }

        public Stream GetAsciiStream(string id)
        {
            if (id == null)
                return null;

            return assemblies
                .Select(x => x.GetManifestResourceStream(id))
                .FirstOrDefault(x => x != null);
        }
    }
}