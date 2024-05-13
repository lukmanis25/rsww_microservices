using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Events
{
    [Message("reservation")]
    public class ReservationPurchasePending : IEvent
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime ReservedUntil { get; set; }
    }
}
