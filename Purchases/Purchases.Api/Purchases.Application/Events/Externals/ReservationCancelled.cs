using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Events.Externals
{
    [Message("reservation")]
    public class ReservationCancelled : IEvent
    {
        public Guid ReservationId { get; set; }
    }
}
