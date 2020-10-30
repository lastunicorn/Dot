using System;
using DustInTheWind.Dot.Application;

namespace DustInTheWind.Dot.Bootstrapping.MicrosoftDependencyInjection
{
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly IServiceProvider serviceProvider;

        public UseCaseFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public T Create<T>()
            where T: class
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}