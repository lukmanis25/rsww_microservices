using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Entities
{
    public class ResourceReservation
    {
        public Guid ResourceId { get; set; }
        public ReservationStatus Status { get; set; }

    }
}
