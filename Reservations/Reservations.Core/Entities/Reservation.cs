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
        public ResourceReservation TransportTo { get; protected set; }
        public ResourceReservation TransportBack { get; protected set; }
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
            ResourceReservation transportTo,
            ResourceReservation transportBack, 
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
            TransportTo = transportTo;
            TransportBack = transportBack;
            IsPromotion = isPromotion;
            CreationDateTime = creationDateTime;
            TotalPrice = totalPrice;
            Version = version;
        }

        private static void ValidHotelRooms(HotelRoomReservation hotelRoom, int numberOfPeople)
        {
            if (hotelRoom is null || hotelRoom.Rooms is null)
            {
                throw new InvalidReservationException();
            }

            int roomsCapacity = hotelRoom.Rooms.Sum(room => room.Capacity * room.Amount);
            if(roomsCapacity < numberOfPeople)
            {
                throw new HotelRoomCapacityExceededException(numberOfPeople, roomsCapacity);
            }
        }

        private static float CalculateTotalPrice(
            float hotelRoomPrice, 
            float transportToPrice, 
            float transportBackPrice, 
            MealType mealType, 
            bool isPromotion,
            int numberOfAdults,
            int numberOfChildrenTo3,
            int numberOfChildrenTo10,
            int numberOfChildrenTo18
            )
        {
            float transportPrice = numberOfAdults * (transportToPrice + transportBackPrice);
            transportPrice += numberOfChildrenTo18 * (transportToPrice * 0.8f + transportBackPrice * 0.8f);
            transportPrice += numberOfChildrenTo10 * (transportToPrice * 0.5f + transportBackPrice * 0.5f);
            transportPrice += numberOfChildrenTo3 * (transportToPrice * 0.2f + transportBackPrice * 0.2f);
           
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
            Guid? transportToId = null, 
            float transportToPrice = 0,
            Guid? transportBackId = null,
            float transportBackPrice = 0,
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

            var transportTo = transportToId != null && transportBackId != Guid.Empty
                ? new ResourceReservation { ResourceId = transportToId.Value, Status = ReservationStatus.PendingReservationApproval, 
                    Price = transportToPrice}
                : null;

            var transportBack = transportBackId != null && transportBackId != Guid.Empty
                ? new ResourceReservation { ResourceId = transportBackId.Value, Status = ReservationStatus.PendingReservationApproval,
                    Price = transportBackPrice
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
                transportTo: transportTo,
                transportBack: transportBack,
                creationDateTime: creationDateTime,
                isPromotion: isPromotion,
                totalPrice: CalculateTotalPrice(hotelRoomPrice, transportToPrice, transportBackPrice, mealType, 
                    isPromotion, numberOfAdults, numberOfChildrenTo3, numberOfChildrenTo10, numberOfChildrenTo18)
                );
            reservation.AddEvent(new ReservationCreated { Reservation = reservation});
            return reservation;
        }

        public int GetNumberOfPeople()
        {
            return NumberOfAdults + NumberOfChildrenTo3 + NumberOfChildrenTo10 + NumberOfChildrenTo18;
        }

        public bool IsCancelled()
        {
            return TransportBack.Status == ReservationStatus.Cancelled || TransportTo.Status == ReservationStatus.Cancelled
                || HotelRoom.Status == ReservationStatus.Cancelled;
        }

        public bool IsPurchased()
        {
            return TransportBack.Status == ReservationStatus.Purchased && TransportTo.Status == ReservationStatus.Purchased
                && HotelRoom.Status == ReservationStatus.Purchased;
        }

        public bool AreAllResourceReserved()
        {
            return TransportBack.Status == ReservationStatus.Reserved && TransportTo.Status == ReservationStatus.Reserved
                && HotelRoom.Status == ReservationStatus.Reserved;
        }

        public void CancelReservation()
        {
            TransportBack.Status = ReservationStatus.Cancelled;
            TransportTo.Status = ReservationStatus.Cancelled;
            HotelRoom.Status = ReservationStatus.Cancelled;
            AddEvent(new ReservationCancelled { Reservation = this });
        }

        public void PurchaseReservation()
        {
            TransportBack.Status = ReservationStatus.Purchased;
            TransportTo.Status = ReservationStatus.Purchased;
            HotelRoom.Status = ReservationStatus.Purchased;
            AddEvent(new ReservationPurchased { Reservation = this });
        }

        public void ReserveTransport(Guid resourceId)
        {
            if(TransportTo.ResourceId == resourceId)
            {
                TransportTo.Status = ReservationStatus.Reserved;
            }
            else if(TransportBack.ResourceId == resourceId)
            {
                TransportBack.Status = ReservationStatus.Reserved;
            }
            AddEvent(new ResourceReserved { Reservation = this });
        }
        public void ReserveHotel(Guid resourceId)
        {
            HotelRoom.Status = ReservationStatus.Reserved;
            AddEvent(new ResourceReserved { Reservation = this });
        }

    }
}
