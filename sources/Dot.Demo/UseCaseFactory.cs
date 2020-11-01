using System;
using DustInTheWind.Dot.Application;
using Ninject;

namespace DustInTheWind.Dot.Demo
{
    internal class UseCaseFactory : IUseCaseFactory
    {
        private readonly IKernel kernel;

        public UseCaseFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public T Create<T>()
            where T : class
        {
            return kernel.Get<T>();
        }
    }
}