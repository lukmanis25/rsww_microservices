using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Payments.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.Events.Externals
{
    [Message("purchase")]
    public class PurchasePendingCreated : IEvent
    {
        public Guid PurchaseId { get; set; }
        public float Price { get; set; }
    }
}
