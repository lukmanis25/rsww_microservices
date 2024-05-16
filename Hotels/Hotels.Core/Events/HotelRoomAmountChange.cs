using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Core.Events
{
    public class HotelRoomAmountChange
    {
        public Guid HotelId { get; set; }
        public Room Room { get; set; }
    }
}
