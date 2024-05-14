using Purchases.Core.Exceptions;
using System;

namespace Reservations.Core.Exceptions
{
    public class InvalidPaymentStatusException : DomainException
    {
        public override string Code { get; } = "invalid_payment_status";

        public InvalidPaymentStatusException() : base($"Invalid payment status") { }
    }
}
