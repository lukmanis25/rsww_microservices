using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External
{
    [Message("purchases")] //to co będzie w servisie Purchase w appsettings->rabbitMq->exchange->name
    public class PurchaseCompleted : IEvent
    {
        public Guid PurchaseId { get; set; }
        public Guid ReservationId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

    }
    public enum PaymentStatus
    {
        Pending,
        Failed,
        Accepted
    }

}
