using Convey.CQRS.Commands;
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
        public Guid HotelId { get; set; }
        public MealType MealType { get; set; }
        public Room Room { get; set; }
        public float HotelRoomPrice { get; set; }
        public Guid? TravelToId { get; set; }
        public float TravelToPrice { get; set; }
        public Guid? TravelBackId { get; set; }
        public float TravelBackPrice { get; set; }
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
            HotelId = reservationWithoutId.HotelId;
            MealType = reservationWithoutId.MealType;
            Room = reservationWithoutId.Room;
            HotelRoomPrice = reservationWithoutId.HotelRoomPrice;
            TravelToId = reservationWithoutId.TravelToId;
            TravelToPrice = reservationWithoutId.TravelToPrice;
            TravelBackId = reservationWithoutId.TravelBackId;
            TravelBackPrice = reservationWithoutId.TravelBackPrice;
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
        public Guid HotelId { get; set; }
        public MealType MealType { get; set; }
        public Room Room { get; set; }
        public float HotelRoomPrice { get; set; }
        public Guid? TravelToId { get; set; }
        public float TravelToPrice { get; set; }
        public Guid? TravelBackId { get; set; }
        public float TravelBackPrice { get; set; }
        public string PromotionCode { get; set; }
    }
}
