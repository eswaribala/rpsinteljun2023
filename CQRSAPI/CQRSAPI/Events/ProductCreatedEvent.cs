using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSAPI.Events
{
    public class ProductCreatedEvent : IEvent
    {
    
        public long ProductNo { get; set; }
        public string ProductName { get; set; }
        public long Cost { get; set; }
    }
}
