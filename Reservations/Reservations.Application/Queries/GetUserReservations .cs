using Convey.CQRS.Queries;
using Reservations.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Queries
{
    public class GetUserReservations : IQuery<IEnumerable<ReservationDto>>
    {
        public Guid CustomerId { get; set; }
    }
}
