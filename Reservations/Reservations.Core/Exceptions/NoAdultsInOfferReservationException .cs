using System;

namespace Reservations.Core.Exceptions
{
    public class NoAdultsInReservationException : DomainException
    {
        public override string Code { get; } = "no_adults_in_reservation";

        public NoAdultsInReservationException() : base($"No adults in reservation") { }
    }
}
