using Reservations.Core.Events;
using Reservations.Core.Exceptions;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Entities
{
    public class Reservation : AggregateRoot
    {
        public Guid CustomerId { get; protected set; }
        public int NumberOfAdults { get; protected set; }
        public int NumberOfChildrenTo3 { get; protected set; }
        public int NumberOfChildrenTo10 { get; protected set; }
        public int NumberOfChildrenTo18 { get; protected set; }
        public Tour Tour { get; protected set; }
        public HotelRoomReservation HotelRoom { get; protected set; }
        public ResourceReservation TravelTo { get; protected set; }
        public ResourceReservation TravelBack { get; protected set; }
        public bool IsPromotion { get; protected set; }
        public float TotalPrice { get; protected set; }
        public DateTime CreationDateTime { get; protected set; }

        public Reservation(
            Guid id, 
            Guid customerId, 
            int numberOfAdults, 
            int numberOfChildrenTo3, 
            int numberOfChildrenTo10,
            int numberOfChildrenTo18, 
            Tour tour,
            HotelRoomReservation hotelRoom,  
            ResourceReservation travelTo,
            ResourceReservation travelBack, 
            bool isPromotion, float totalPrice, 
            DateTime creationDateTime,                        
            int version = 0)
        {

            if (customerId == Guid.Empty)
            {
                throw new InvalidReservationException();
            }
            if (numberOfAdults <= 0)
            {
                throw new NoAdultsInReservationException();
            }
            ValidHotelRooms(hotelRoom, numberOfAdults + numberOfChildrenTo3 + numberOfChildrenTo10 + numberOfChildrenTo18);
            Id = id;
            CustomerId = customerId;
            NumberOfAdults = numberOfAdults;
            NumberOfChildrenTo3 = numberOfChildrenTo3;
            NumberOfChildrenTo10 = numberOfChildrenTo10;
            NumberOfChildrenTo18 = numberOfChildrenTo18;
            Tour = tour;
            HotelRoom = hotelRoom;
            TravelTo = travelTo;
            TravelBack = travelBack;
            IsPromotion = isPromotion;
            CreationDateTime = creationDateTime;
            Version = version;
            TotalPrice = totalPrice;

        }

        private static void ValidHotelRooms(HotelRoomReservation hotelRoom, int numberOfPeople)
        {
            if (hotelRoom is null || hotelRoom.Rooms is null)
            {
                throw new InvalidReservationException();
            }

            int roomsCapacity = hotelRoom.Rooms.Sum(room => room.Capacity * room.Count);
            if(roomsCapacity < numberOfPeople)
            {
                throw new HotelRoomCapacityExceededException(numberOfPeople, roomsCapacity);
            }
        }

        private static float CalculateTotalPrice(
            float hotelRoomPrice, 
            float travelToPrice, 
            float travelBackPrice, 
            MealType mealType, 
            bool isPromotion,
            int numberOfAdults,
            int numberOfChildrenTo3,
            int numberOfChildrenTo10,
            int numberOfChildrenTo18
            )
        {
            float transportPrice = numberOfAdults * (travelToPrice + travelBackPrice);
            transportPrice += numberOfChildrenTo18 * (travelToPrice * 0.8f + travelBackPrice * 0.8f);
            transportPrice += numberOfChildrenTo10 * (travelToPrice * 0.5f + travelBackPrice * 0.5f);
            transportPrice += numberOfChildrenTo3 * (travelToPrice * 0.2f + travelBackPrice * 0.2f);
           
            int peopleNumber = numberOfAdults + numberOfChildrenTo3 + numberOfChildrenTo10 + numberOfChildrenTo18;
            float hotelPrice = hotelRoomPrice;
            if (mealType == MealType.AllInclusive)
            {
                hotelPrice += 300 * peopleNumber;
            }
            float totalPrice = transportPrice + hotelPrice;
            if (isPromotion)
            {
                totalPrice *= 0.5f;

            }
            return totalPrice;
        }

        public static Reservation Create(
            Guid id, 
            Guid customerId, 
            int numberOfAdults, 
            int numberOfChildrenTo3,
            int numberOfChildrenTo10,
            int numberOfChildrenTo18, 
            Tour tour,
            Guid hotelId, 
            MealType mealType,
            IEnumerable<Room> rooms, 
            float hotelRoomPrice, 
            Guid? travelToId = null, 
            float travelToPrice = 0,
            Guid? travelBackId = null,
            float travelBackPrice = 0,
            string promotionCode = "" 
            )
        {
            var hotelRoom = new HotelRoomReservation
            {
                ResourceId = hotelId,
                Rooms = rooms,
                Status = ReservationStatus.PendingReservationApproval,
                MealType = mealType,
                Price = hotelRoomPrice,
            };

            var isPromotion = promotionCode == "PROMO";

            var travelTo = travelToId != null && travelBackId != Guid.Empty
                ? new ResourceReservation { ResourceId = travelToId.Value, Status = ReservationStatus.PendingReservationApproval, 
                    Price = travelToPrice}
                : null;

            var travelBack = travelBackId != null && travelBackId != Guid.Empty
                ? new ResourceReservation { ResourceId = travelBackId.Value, Status = ReservationStatus.PendingReservationApproval,
                    Price = travelBackPrice
                }
                : null;

            var creationDateTime = DateTime.Now;

            var reservation = new Reservation(
                id: id, 
                customerId: customerId, 
                numberOfAdults: numberOfAdults,
                numberOfChildrenTo3: numberOfChildrenTo3,
                numberOfChildrenTo10: numberOfChildrenTo10,
                numberOfChildrenTo18: numberOfChildrenTo18,
                tour: tour,
                hotelRoom: hotelRoom,
                travelTo: travelTo,
                travelBack: travelBack,
                creationDateTime: creationDateTime,
                isPromotion: isPromotion,
                totalPrice: CalculateTotalPrice(hotelRoomPrice, travelToPrice, travelBackPrice, mealType, 
                    isPromotion, numberOfAdults, numberOfChildrenTo3, numberOfChildrenTo10, numberOfChildrenTo18)
                );
            reservation.AddEvent(new ReservationCreated { Reservation = reservation});
            return reservation;
        }

    }
}
