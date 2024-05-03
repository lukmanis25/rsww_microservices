using Reservations.Core.Exceptions;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Entities
{
    public class OfferReservation : AggregateRoot
    {
        public Guid CustomerId { get; protected set; }
        public int NumberOfAdults { get; protected set; }
        public int NumberOfChildren { get; protected set; }
        public HotelRoomsReservation HotelRooms { get; protected set; }
        public ResourceReservation TravelTo { get; protected set; }
        public ResourceReservation TravelBack { get; protected set; }
        public DateTime CreationDateTime { get; protected set; }

        public OfferReservation(Guid customerId, int numberOfAdults, int numberOfChildren,
                            Guid hotelId, IEnumerable<Room> rooms,  Guid? travelToId = null,
                            Guid? travelBackId = null)
        {


            var hotelRooms = new HotelRoomsReservation
            {
                ResourceId = hotelId,
                Rooms = rooms,
                Status = ReservationStatus.New
            };

            var travelTo = travelToId != null
                ? new ResourceReservation { ResourceId = travelToId.Value, Status = ReservationStatus.New }
                : null;

            var travelBack = travelBackId != null
                ? new ResourceReservation { ResourceId = travelBackId.Value, Status = ReservationStatus.New }
                : null;

            if (customerId == Guid.Empty || hotelId == Guid.Empty || numberOfAdults <= 0)
            {
                throw new InvalidOfferReservationException();
            }
            ValidHotelRooms(hotelRooms, numberOfAdults + numberOfChildren);

            CustomerId = customerId;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
            HotelRooms = hotelRooms;
            TravelTo = travelTo;
            TravelBack = travelBack;
            CreationDateTime = DateTime.Now;
        }

        private static void ValidHotelRooms(HotelRoomsReservation hotelRooms, int numberOfPeople)
        {
            if (hotelRooms is null || hotelRooms.Rooms is null)
            {
                throw new InvalidOfferReservationException();
            }

            int roomsOccupancy = hotelRooms.Rooms.Sum(room => room.Occupancy);
            if(roomsOccupancy < numberOfPeople)
            {
                throw new HotelRoomsOccupancyExceededException(numberOfPeople, roomsOccupancy);
            }
        }

        public static OfferReservation Create(Guid customerId, int numberOfAdults, int numberOfChildren,
                            Guid hotelId, IEnumerable<Room> rooms, Guid? travelToId = null,
                            Guid? travelBackId = null)
        {
            var offerReservation = new OfferReservation(customerId, numberOfAdults, numberOfChildren, 
                hotelId, rooms, travelToId, travelBackId);
            offerReservation.AddEvent(new OfferReservationCreated(offerReservation));
            return offerReservation;
        }

    }
}
