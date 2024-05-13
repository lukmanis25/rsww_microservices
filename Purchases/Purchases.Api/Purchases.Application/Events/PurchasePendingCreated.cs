using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Events
{
    [Contract]
    public class PurchasePendingCreated : IEvent
    {
        public Guid PurchaseId { get; set; }
        public float Price { get; set; }
    }
}
