using System;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation;
using Ninject;

namespace DustInTheWind.Dot.Demo
{
    internal class CommandFactory : ICommandFactory
    {
        private readonly IKernel kernel;

        public CommandFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public T Create<T>()
            where T : ICommand
        {
            return kernel.Get<T>();
        }
    }
}