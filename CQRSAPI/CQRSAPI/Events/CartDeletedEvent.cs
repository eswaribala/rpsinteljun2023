﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSAPI.Events
{
    public class CartDeletedEvent : IEvent
    {
        public long CartId { get; set; }
    }
}
