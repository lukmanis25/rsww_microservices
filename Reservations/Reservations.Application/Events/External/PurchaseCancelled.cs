using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External
{
    [Message("purchase")]
    public class PurchaseCancelled : IEvent
    {
        public Guid PurchaseId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
