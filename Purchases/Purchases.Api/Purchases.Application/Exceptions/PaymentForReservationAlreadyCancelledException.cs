using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Exceptions
{
    public class PaymentForReservationAlreadyCancelledException : AppException
    {
        public override string Code { get; } = "payment_for_reservation_already_cancelled";
        public Guid Id { get; }

        public PaymentForReservationAlreadyCancelledException(Guid id) : base($"Payment for reservation: {id} already cancelled")
            => Id = id;
    }
}
