﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Constant
{
    static public class RabbitMQSettings
    {
        public const string Bill_OrderCreatedEventQueue = "bill-order-created-event-queue";
        public const string Payment_OrderCreatedEventQueue = "payment-order-created-event-queue";
    }
}
