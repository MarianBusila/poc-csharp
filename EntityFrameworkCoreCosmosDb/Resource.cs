using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreCosmosDb
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }

        public string Country { get; set; }
    }
}
