using Convey.CQRS.Commands;
using Reservations.Core.Entities;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Commands
{
    public class AddReservation : ICommand
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildrenTo3 { get; set; }
        public int NumberOfChildrenTo10 { get; set; }
        public int NumberOfChildrenTo18 { get; set; }
        public Tour Tour { get; set; }
        public Guid HotelId { get; set; }
        public MealType MealType { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public float HotelRoomPrice { get; set; }
        public Guid TransportToId { get; set; }
        public float TransportToPrice { get; set; }
        public Guid TransportBackId { get; set; }
        public float TransportBackPrice { get; set; }
        public string PromotionCode { get; set; }

        public AddReservation(
            Guid id,
            AddReservationWithoutId reservationWithoutId)
        {
            Id = id;
            CustomerId = reservationWithoutId.CustomerId;
            NumberOfAdults = reservationWithoutId.NumberOfAdults;
            NumberOfChildrenTo3 = reservationWithoutId.NumberOfChildrenTo3;
            NumberOfChildrenTo10 = reservationWithoutId.NumberOfChildrenTo10;
            NumberOfChildrenTo18 = reservationWithoutId.NumberOfChildrenTo18;
            Tour = reservationWithoutId.Tour;
            HotelId = reservationWithoutId.HotelId;
            MealType = reservationWithoutId.MealType;
            Rooms = reservationWithoutId.Rooms;
            HotelRoomPrice = reservationWithoutId.HotelRoomPrice;
            TransportToId = reservationWithoutId.TransportToId;
            TransportToPrice = reservationWithoutId.TransportToPrice;
            TransportBackId = reservationWithoutId.TransportBackId;
            TransportBackPrice = reservationWithoutId.TransportBackPrice;
            PromotionCode = reservationWithoutId.PromotionCode;
        }
    }
    public class AddReservationWithoutId
    {
        public Guid CustomerId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildrenTo3 { get; set; }
        public int NumberOfChildrenTo10 { get; set; }
        public int NumberOfChildrenTo18 { get; set; }
        public Tour Tour { get; set; }
        public Guid HotelId { get; set; }
        public MealType MealType { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public float HotelRoomPrice { get; set; }
        public Guid TransportToId { get; set; }
        public float TransportToPrice { get; set; }
        public Guid TransportBackId { get; set; }
        public float TransportBackPrice { get; set; }
        public string PromotionCode { get; set; }
    }
}
