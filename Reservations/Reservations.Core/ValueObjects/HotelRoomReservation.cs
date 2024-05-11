using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.ValueObjects
{
    public enum MealType
    {
        Regular,
        AllInclusive
    }
    public class HotelRoomReservation : ResourceReservation
    {
        public Room Room { get; set; }
        public MealType MealType { get; set; }
    }
}
