using DustInTheWind.Dot.Bootstrapping;
using DustInTheWind.Dot.Ninject;

namespace DustInTheWind.Dot.Demo
{
    internal class Bootstrapper : BootstrapperBase
    {
        protected override IServicesContainer CreateServicesContainer()
        {
            return new NinjectServicesContainer();
        }

        protected override void ConfigureServices(IServicesContainer servicesContainer)
        {
            base.ConfigureServices(servicesContainer);

            servicesContainer.ConfigureFactories();
        }
    }
}