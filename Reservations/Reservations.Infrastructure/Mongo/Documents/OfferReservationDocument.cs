using Convey.Types;
using Reservations.Core.Entities;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Infrastructure.Mongo.Documents
{
    internal sealed class OfferReservationDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OffertId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public HotelRoomReservation HotelRooms { get; set; }
        public ResourceReservation TravelTo { get; set; }
        public ResourceReservation TravelBack { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int Version { get; set; }
    }
}
