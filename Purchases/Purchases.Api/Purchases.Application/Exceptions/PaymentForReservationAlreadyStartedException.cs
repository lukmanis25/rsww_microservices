using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Exceptions
{
    public class PaymentForReservationAlreadyStartedException : AppException
    {
        public override string Code { get; } = "payment_for_reservation_already_started";
        public Guid Id { get; }

        public PaymentForReservationAlreadyStartedException(Guid id) : base($"Payment for reservation: {id} already started")
            => Id = id;
    }
}
