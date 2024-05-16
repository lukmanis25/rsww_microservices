using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transports.Application.Events
{
    [Contract]
    public class TransportReservationRejected : IEvent
    {
        public Guid TransportId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
