using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Entities
{
    public class ServiceCartWrite
    {
        public string UserName { get; set; }

        public List<string> serviceIds { get; set; }
    }
}
