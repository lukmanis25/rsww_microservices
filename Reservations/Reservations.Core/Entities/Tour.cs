using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Entities
{
    public class Tour
    {
        public Guid TourId { get; set; }
        public DateTime TourStartDatetime { get; set; }
        public DateTime TourEndDateTime { get; set; }
        public string TourPlaceOfDeparture { get; set; }
        public string TourDestination { get; set; }
    }
}
