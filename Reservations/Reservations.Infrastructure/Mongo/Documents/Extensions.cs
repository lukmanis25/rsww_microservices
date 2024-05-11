using Reservations.Application.DTO;
using Reservations.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static Reservation AsEntity(this OfferReservationDocument document)
            => new OfferReservation(document.Id, document.CustomerId, document.OffertId,
                        document.NumberOfAdults, document.NumberOfChildren, document.HotelRooms,
                        document.TravelTo, document.TravelBack, document.CreationDateTime, document.Version);


        public static OfferReservationDocument AsDocument(this Reservation entity)
        {
            return new OfferReservationDocument
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                OffertId = entity.OffertId,
                NumberOfAdults = entity.NumberOfAdults,
                NumberOfChildren = entity.NumberOfChildren,
                HotelRooms = entity.HotelRoom,
                TravelTo = entity.TravelTo,
                TravelBack = entity.TravelBack,
                CreationDateTime = entity.CreationDateTime,
                Version = entity.Version
            };
        }

        public static OfferReservationDto AsDto(this OfferReservationDocument document)
        {
            return new OfferReservationDto
            {
                Id = document.Id,
                CustomerId = document.CustomerId,
                OffertId = document.OffertId,
                NumberOfAdults = document.NumberOfAdults,
                NumberOfChildren = document.NumberOfChildren,
                HotelRooms = document.HotelRooms,
                TravelTo = document.TravelTo,
                TravelBack = document.TravelBack,
                CreationDateTime = document.CreationDateTime,
            };
        }
    }
}
