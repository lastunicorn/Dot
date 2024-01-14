using System;
using DustInTheWind.Dot.Presentation;
using DustInTheWind.Dot.Presentation.ConsoleHelpers.UIControls;
using Ninject;

namespace DustInTheWind.Dot.Ninject
{
    public class CommandFactory : ICommandFactory
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