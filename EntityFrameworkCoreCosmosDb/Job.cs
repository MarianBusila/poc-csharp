using System;
using System.Collections.Generic;

namespace EntityFrameworkCoreCosmosDb
{
    public class Job
    {
        public Guid Id { get; set; }

        public string Department { get; set; }
        public Address Address { get; set; }

        public List<Contact> Contacts { get; set; }
        
        public Guid AssignedResourceId { get; set; }
        public Resource AssignedResource { get; set; }
        
    }
}
