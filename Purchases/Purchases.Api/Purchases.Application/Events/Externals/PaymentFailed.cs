using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Events
{
    [Message("payment")]
    public class PaymentFailed : IEvent
    {
        public Guid PurchaseId { get; set; }
    }
}
