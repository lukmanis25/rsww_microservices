using Reservations.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Events
{
    public class OfferReservationCreated : IDomainEvent
    {
        public OfferReservation OfferReservation { get; }

        public OfferReservationCreated(OfferReservation offerReservation)
            => OfferReservation = offerReservation;
    }
}
