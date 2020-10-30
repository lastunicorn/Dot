using System;
using DustInTheWind.Dot.Domain;
using DustInTheWind.Dot.Presentation;
using Ninject;

namespace DustInTheWind.Dot.Demo
{
    internal class ScreenFactory : IScreenFactory
    {
        private readonly IKernel kernel;

        public ScreenFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public T Create<T>()
        {
            return kernel.Get<T>();
        }
    }
}