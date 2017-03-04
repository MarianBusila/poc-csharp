using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Hosting.Self;

namespace TestNancyTopshelf
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TestNancyTopshelf.IVenueService" />
    public class VenueService : IVenueService
    {
        private NancyHost nancyHost;
        private string hostURL;
        private ILogger logger;

        public VenueService(IConfigurationService configurationService, ILogger logger)
        {
            hostURL = configurationService.GetValue("HostURL");
            this.logger = logger;
        }

        public void Start()
        {
            HostConfiguration hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;
            nancyHost = new NancyHost(new Uri(hostURL), new DefaultNancyBootstrapper(), hostConfigs);
            logger.LogMessage("Start VenueService ...");
            nancyHost.Start();
        }

        public void Stop()
        {
            logger.LogMessage("Stop VenueService ...");
            nancyHost.Stop();
            nancyHost.Dispose();
        }

    }
}
