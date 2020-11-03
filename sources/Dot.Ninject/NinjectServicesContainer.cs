using System;
using DustInTheWind.Dot.Bootstrapping;
using Ninject;

namespace DustInTheWind.Dot.Ninject
{
    public class NinjectServicesContainer : IServicesContainer
    {
        private readonly IKernel kernel;

        public NinjectServicesContainer()
        {
            kernel = new StandardKernel();
        }

        public void AddTransient<TService>()
            where TService : class
        {
            kernel.Bind<TService>().ToSelf();
        }

        public void AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            kernel.Bind<TService>().To<TImplementation>();
        }

        public void AddTransient(Type serviceType, Type implementationType)
        {
            kernel.Bind(serviceType).To(implementationType);
        }

        public void AddTransient<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            kernel.Bind<TService>().ToMethod(context => implementationFactory(kernel));
        }

        public void AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            kernel.Bind<TService>().ToMethod(context => implementationFactory(kernel));
        }

        public void AddTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            kernel.Bind(serviceType).ToMethod(context => implementationFactory(kernel));
        }

        public void AddSingleton<TService>()
            where TService : class
        {
            kernel.Bind<TService>().ToSelf().InSingletonScope();
        }

        public void AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            kernel.Bind<TService>().To<TImplementation>().InSingletonScope();
        }

        public void AddSingleton(Type serviceType, Type implementationType)
        {
            kernel.Bind(serviceType).To(implementationType).InSingletonScope();
        }

        public void AddSingleton<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            kernel.Bind<TService>().ToMethod(context => implementationFactory(kernel)).InSingletonScope();
        }

        public void AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            kernel.Bind<TService>().ToMethod(context => implementationFactory(kernel)).InSingletonScope();
        }

        public void AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            kernel.Bind(serviceType).ToMethod(context => implementationFactory(kernel)).InSingletonScope();
        }

        public void AddSingleton<TService>(TService implementationInstance)
            where TService : class
        {
            kernel.Bind<TService>().ToConstant(implementationInstance).InSingletonScope();
        }

        public void AddSingleton(Type serviceType, object implementationInstance)
        {
            kernel.Bind(serviceType).ToConstant(implementationInstance).InSingletonScope();
        }

        public IServiceProvider BuildServiceProvider()
        {
            return kernel;
        }
    }
}