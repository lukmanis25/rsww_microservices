using System;

namespace Reservations.Core.Exceptions
{
    public class InvalidOfferReservationException : DomainException
    {
        public override string Code { get; } = "invalid_offer_reservation";

        public InvalidOfferReservationException() : base($"Invalid offer reservation") { }
    }
}
