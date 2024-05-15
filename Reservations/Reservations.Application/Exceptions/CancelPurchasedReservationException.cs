using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Exceptions
{
    public class CancelPurchasedReservationException : AppException
    {
        public override string Code { get; } = "cant_cancel_purchased_reservation";
        public Guid Id { get; }

        public CancelPurchasedReservationException(Guid id) : base($"Cant cancel purchased reservation with id: {id}")
            => Id = id;
    }
}
