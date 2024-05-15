using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Exceptions
{
    public class ReservationAlreadyCancelledException : AppException
    {
        public override string Code { get; } = "reservation_already_cancelled";
        public Guid Id { get; }

        public ReservationAlreadyCancelledException(Guid id) : base($"Reservation: {id} already cancelled")
            => Id = id;
    }
}
