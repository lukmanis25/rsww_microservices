using System;

namespace Reservations.Core.Exceptions
{
    public class NoAdultsInOfferReservationException : DomainException
    {
        public override string Code { get; } = "no_adults_offer_reservation";

        public NoAdultsInOfferReservationException() : base($"No adults in offer reservation") { }
    }
}
