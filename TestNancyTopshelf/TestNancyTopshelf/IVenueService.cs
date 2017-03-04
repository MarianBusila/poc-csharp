using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNancyTopshelf
{
    /// <summary>
    /// Classes implementing this interface provide methods for getting
    /// or manipulating venue data.
    /// </summary>
    public interface IVenueService
    {
        /// <summary>
        /// Starts the service.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the service.
        /// </summary>
        void Stop();
    }
}
