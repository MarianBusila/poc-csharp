using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Ninject;

namespace TestNancyTopshelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.UseNinject(new VenueServiceModule());

                hostConfigurator.Service<IVenueService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsingNinject();
                    serviceConfigurator.WhenStarted(service => service.Start());
                    serviceConfigurator.WhenStopped(service => service.Stop());
                });

                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.SetDescription("The venue service provides venue data");
                hostConfigurator.SetDisplayName("Venue Service");
                hostConfigurator.SetServiceName("VenueService");
             });
        }
    }
}
