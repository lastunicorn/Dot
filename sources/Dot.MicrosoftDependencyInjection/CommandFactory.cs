using System;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation;

namespace DustInTheWind.Dot.MicrosoftDependencyInjection
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public T Create<T>()
            where T : ICommand
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}