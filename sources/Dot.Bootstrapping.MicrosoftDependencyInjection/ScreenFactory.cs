using System;
using DustInTheWind.Dot.Presentation;

namespace DustInTheWind.Dot.Bootstrapping.MicrosoftDependencyInjection
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ScreenFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public T Create<T>()
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}