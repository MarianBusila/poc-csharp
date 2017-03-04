using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace TestNancyTopshelf
{
    public class VenuesModule : NancyModule
    {
        List<Venue> venues;

        public VenuesModule(IConfigurationService configurationService)
        {
            Get["/venues"] = parameters =>
            {
                var venues = new List<Venue>()
                {
                    new Venue() { Name = "Avatar", NoOfDays = 1, Place = "Cinema" } ,
                    new Venue() { Name = "Confoo", NoOfDays = 3, Place = "Bonaventure" }
                };
                return Response.AsJson(venues);
            };
        }
    }
}
