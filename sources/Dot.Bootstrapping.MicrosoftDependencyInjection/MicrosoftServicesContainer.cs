using System;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.Dot.Bootstrapping.MicrosoftDependencyInjection
{
    public class MicrosoftServicesContainer : IServicesContainer
    {
        private readonly ServiceCollection serviceCollection;

        public MicrosoftServicesContainer()
        {
            serviceCollection = new ServiceCollection();
        }

        public void AddTransient<TService>()
            where TService : class
        {
            serviceCollection.AddTransient<TService>();
        }

        public void AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.AddTransient<TService, TImplementation>();
        }

        public void AddTransient(Type serviceType, Type implementationType)
        {
            serviceCollection.AddTransient(serviceType, implementationType);
        }

        public void AddTransient<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class 
            where TImplementation : class, TService
        {
            serviceCollection.AddTransient<TService, TImplementation>(implementationFactory);
        }

        public void AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory) 
            where TService : class
        {
            serviceCollection.AddTransient<TService>(implementationFactory);
        }

        public void AddTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.AddTransient(serviceType, implementationFactory);
        }

        public void AddSingleton<TService>()
            where TService : class
        {
            serviceCollection.AddSingleton<TService>();
        }

        public void AddSingleton<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService
        {
            serviceCollection.AddSingleton<TService, TImplementation>();
        }

        public void AddSingleton(Type serviceType, Type implementationType)
        {
            serviceCollection.AddSingleton(serviceType, implementationType);
        }

        public void AddSingleton<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory) 
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.AddSingleton<TService, TImplementation>(implementationFactory);
        }

        public void AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            serviceCollection.AddSingleton<TService>(implementationFactory);
        }

        public void AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.AddSingleton(serviceType, implementationFactory);
        }

        public void AddSingleton<TService>(TService implementationInstance)
            where TService : class
        {
            serviceCollection.AddSingleton<TService>(implementationInstance);
        }

        public void AddSingleton(Type serviceType, object implementationInstance)
        {
            serviceCollection.AddSingleton(serviceType, implementationInstance);
        }

        public IServiceProvider BuildServiceProvider()
        {
            return serviceCollection.BuildServiceProvider();
        }
    }
}