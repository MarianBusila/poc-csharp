using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject.Web.Common;

namespace TestNancyTopshelf
{
    public class VenueServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ILogger>().To<ConsoleLogger>().InRequestScope();
            this.Bind<IConfigurationService>().To<ConfigurationService>().InRequestScope();
            this.Bind<IVenueService>().To<VenueService>().InRequestScope();
        }
    }
}
