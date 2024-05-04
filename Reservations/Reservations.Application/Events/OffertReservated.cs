using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events
{
    [Contract]
    public class OffertReservated : IEvent
    {
        public Guid OffertId { get; set; }
        public Guid CustomerId { get; set; }
        public OffertReservated(Guid offertId, Guid customerId)
        {
            OffertId = offertId;
            CustomerId = customerId;
        }
    }
}
