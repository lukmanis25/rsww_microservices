using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Entities
{
    public class HotelRoomsReservation : ResourceReservation
    {
        public IEnumerable<Room> Rooms { get; set; }
    }
}
