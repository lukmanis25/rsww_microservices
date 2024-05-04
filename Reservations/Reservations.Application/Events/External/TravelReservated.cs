using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External
{
    [Message("travels")] //to co będzie w servisie Travel w appsettings->rabbitMq->exchange->name
    public class TravelReservated : IEvent
    {
        public Guid TravelId { get; set; }
        public Guid ReservationId { get; set; }
        public int ReservatedSeatNumber { get; set; }

        public TravelReservated(Guid travelId, Guid reservationId, int reservatedSeatNumber)
        {
            TravelId = travelId;
            ReservationId = reservationId;
            ReservatedSeatNumber = reservatedSeatNumber;
        }

    }
}
