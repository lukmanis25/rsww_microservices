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
    public class OfferReservation : AggregateRoot
    {
        public Guid CustomerId { get; protected set; }
        public Guid OffertId { get; set; }
        public int NumberOfAdults { get; protected set; }
        public int NumberOfChildren { get; protected set; }
        public HotelRoomsReservation HotelRooms { get; protected set; }
        public ResourceReservation TravelTo { get; protected set; }
        public ResourceReservation TravelBack { get; protected set; }
        public DateTime CreationDateTime { get; protected set; }

        public OfferReservation(Guid id, Guid customerId, Guid offertId, int numberOfAdults, int numberOfChildren,
                            HotelRoomsReservation hotelRooms,  ResourceReservation travelTo,
                            ResourceReservation travelBack, DateTime creationDateTime, int version = 0)
        {

            if (customerId == Guid.Empty || offertId == Guid.Empty)
            {
                throw new InvalidOfferReservationException();
            }
            if(numberOfAdults <= 0)
            {
                throw new NoAdultsInOfferReservationException();
            }
            ValidHotelRooms(hotelRooms, numberOfAdults + numberOfChildren);
            Id = id;
            OffertId = offertId;
            CustomerId = customerId;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
            HotelRooms = hotelRooms;
            TravelTo = travelTo;
            TravelBack = travelBack;
            CreationDateTime = creationDateTime;
            Version = version;
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

        public static OfferReservation Create(Guid customerId, Guid offerId, int numberOfAdults, int numberOfChildren,
                            Guid hotelId, IEnumerable<Room> rooms, Guid? travelToId = null,
                            Guid? travelBackId = null)
        {
            var id = Guid.NewGuid();

            var hotelRooms = new HotelRoomsReservation
            {
                ResourceId = hotelId,
                Rooms = rooms,
                Status = ReservationStatus.New
            };

            var travelTo = travelToId != null && travelBackId != Guid.Empty
                ? new ResourceReservation { ResourceId = travelToId.Value, Status = ReservationStatus.New }
                : null;

            var travelBack = travelBackId != null && travelBackId != Guid.Empty
                ? new ResourceReservation { ResourceId = travelBackId.Value, Status = ReservationStatus.New }
                : null;

            var createDateTime = DateTime.Now;

            var offerReservation = new OfferReservation(id, customerId, offerId, numberOfAdults, numberOfChildren, 
                hotelRooms, travelTo, travelBack, createDateTime);
            offerReservation.AddEvent(new  OfferReservationCreated(offerReservation));
            return offerReservation;
        }

    }
}
