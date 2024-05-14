using System;

namespace Reservations.Core.Exceptions
{
    public class InvalidReservationStatusException : DomainException
    {
        public override string Code { get; } = "invalid_reservation_status";

        public InvalidReservationStatusException() : base($"Invalid reservation status") { }
    }
}
