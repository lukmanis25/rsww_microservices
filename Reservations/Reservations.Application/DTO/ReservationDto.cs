using Reservations.Core.Entities;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.DTO
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get;  set; }
        public int NumberOfAdults { get;  set; }
        public int NumberOfChildrenTo3 { get;  set; }
        public int NumberOfChildrenTo10 { get;  set; }
        public int NumberOfChildrenTo18 { get;  set; }
        public Tour Tour { get; set; }
        public HotelRoomReservationDto HotelRoom { get;  set; }
        public ResourceReservationDto TransportTo { get;  set; }
        public ResourceReservationDto TransportBack { get;  set; }
        public bool IsPromotion { get;  set; }
        public float TotalPrice { get; set; }
        public DateTime CreationDateTime { get;  set; }
    }

    public class HotelRoomReservationDto : ResourceReservationDto
    {
        public string MealType { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; }
    }

    public class RoomDto
    {
        public int Capacity { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
    }

    public class ResourceReservationDto
    {
        public Guid ResourceId { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
    }
}
