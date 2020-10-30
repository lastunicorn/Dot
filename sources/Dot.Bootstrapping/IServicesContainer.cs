using System;

namespace DustInTheWind.Dot.Bootstrapping
{
    public interface IServicesContainer
    {
        #region Transient

        void AddTransient<TService>()
            where TService : class;

        void AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void AddTransient(Type serviceType, Type implementationType);

        void AddTransient<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService;

        void AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class;

        void AddTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory);

        #endregion

        #region Singleton

        void AddSingleton<TService>()
            where TService : class;

        void AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void AddSingleton(Type serviceType, Type implementationType);

        void AddSingleton<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService;

        void AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class;

        void AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory);

        void AddSingleton<TService>(TService implementationInstance)
            where TService : class;

        void AddSingleton(Type serviceType, object implementationInstance);

        #endregion

        IServiceProvider BuildServiceProvider();
    }
}