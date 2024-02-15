using System.Reflection;

namespace DustInTheWind.Dot.Presentation.Controls
{
    public class ApplicationInfo
    {
        private readonly Assembly assembly;

        public ApplicationInfo()
        {
            assembly = Assembly.GetEntryAssembly();
        }

        public string GetProductName()
        {
            AssemblyProductAttribute assemblyProductAttribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return assemblyProductAttribute.Product;
        }

        public Version GetAssemblyVersion()
        {
            AssemblyName assemblyName = assembly.GetName();
            return assemblyName.Version;
        }

        public string GetAssemblyInformationalVersion()
        {
            AssemblyInformationalVersionAttribute assemblyInformationalVersionAttribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            return assemblyInformationalVersionAttribute.InformationalVersion;
        }
    }
}