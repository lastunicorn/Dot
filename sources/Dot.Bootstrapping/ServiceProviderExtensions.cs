using System;

namespace DustInTheWind.Dot.Bootstrapping
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
            where T : class
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}