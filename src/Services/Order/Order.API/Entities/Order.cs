using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string OrderedBy { get; set; }

        public string CartReferenceId { get; set; }

        public List<Service> OrderedServices { get; set; }
    }
}
