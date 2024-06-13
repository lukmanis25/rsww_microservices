using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events
{
    [Contract]
    public class ReservationPurchasePending : IEvent
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime ReservedUntil { get; set; }
    }
}
