using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Message.Events
{
    public class ServiceEvent
    {
        public decimal Price { get; set; }

        public string ServiceName { get; set; }

        public string ServiceProvider { get; set; }

        public string ProviderEmail { get; set; }

        public decimal ProviderContactNumber { get; set; }
    }
}
