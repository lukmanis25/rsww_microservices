using Convey.Types;
using Reservations.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Infrastructure.Mongo.Documents
{
    internal sealed class ReservationDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildrenTo3 { get; set; }
        public int NumberOfChildrenTo10 { get; set; }
        public int NumberOfChildrenTo18 { get; set; }
        public Tour Tour { get; set; }
        public HotelRoomReservation HotelRoom { get; set; }
        public ResourceReservation TravelTo { get; set; }
        public ResourceReservation TravelBack { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsPromotion { get; set; }
        public float TotalPrice { get; set; }
        public int Version { get; set; }
    }
}
