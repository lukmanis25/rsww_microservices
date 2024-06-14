using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transports.Application.Events
{
    [Contract]
    public class TransportAvailabilityChanged : IEvent
    {
        public Guid TransportId { get; set; }
        public int Amount { get; set; }
    }
}
