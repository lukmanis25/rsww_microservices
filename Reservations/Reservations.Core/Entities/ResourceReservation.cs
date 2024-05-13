using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservations.Core.ValueObjects;

namespace Reservations.Core.Entities
{
    public class ResourceReservation
    {
        public Guid ResourceId { get; set; }
        public ReservationStatus Status { get; set; }
        public float Price { get; set; }

    }
}
