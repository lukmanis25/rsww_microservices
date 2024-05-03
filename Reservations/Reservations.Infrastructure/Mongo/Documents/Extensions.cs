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
        public static OfferReservation AsEntity(this OfferReservationDocument document)
            => new OfferReservation(document.Id, document.CustomerId, document.OffertId,
                        document.NumberOfAdults, document.NumberOfChildren, document.HotelRooms,
                        document.TravelTo, document.TravelBack, document.CreationDateTime, document.Version);


        public static OfferReservationDocument AsDocument(this OfferReservation entity)
        {
            return new OfferReservationDocument
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                OffertId = entity.OffertId,
                NumberOfAdults = entity.NumberOfAdults,
                NumberOfChildren = entity.NumberOfChildren,
                HotelRooms = entity.HotelRooms,
                TravelTo = entity.TravelTo,
                TravelBack = entity.TravelBack,
                CreationDateTime = entity.CreationDateTime,
                Version = entity.Version
            };
        }
    }
}
