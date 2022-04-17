using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Message.Events
{
    public class CartCheckoutEvent : IntegrationBaseEvent
    {
        public string UserName { get; set; }

        public string OrderedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsCancelled { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal ZipCode { get; set; }

        public List<ServiceEvent> OrderedServices { get; set; }
    }
}
