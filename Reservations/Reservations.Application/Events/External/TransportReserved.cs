using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External
{
    [Message("transport")]
    public class TransportReserved : IEvent
    {
        public Guid TransportId { get; set; }
        public Guid ReservationId { get; set; }
        public int Amount { get; set; }

    }
}
