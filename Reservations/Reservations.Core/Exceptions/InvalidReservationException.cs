using System;

namespace Reservations.Core.Exceptions
{
    public class InvalidReservationException : DomainException
    {
        public override string Code { get; } = "invalid_reservation";

        public InvalidReservationException() : base($"Invalid reservation") { }
    }
}
