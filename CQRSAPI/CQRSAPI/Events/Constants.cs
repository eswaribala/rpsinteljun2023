using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSAPI.Events
{
    public class Constants
    {
        public const string QUEUE_CART_CREATED = "cart_created";
        public const string QUEUE_CART_UPDATED = "cart_updated";
        public const string QUEUE_CART_DELETED = "cart_deleted";
    }
}
