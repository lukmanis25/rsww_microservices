using Reservations.Application.DTO;
using Reservations.Core.Entities;
using Reservations.Core.ValueObjects;
using SharpCompress.Common;
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
        public static Reservation AsEntity(this ReservationDocument document)
            => new Reservation(
                id: document.Id,
                customerId: document.CustomerId,
                numberOfAdults: document.NumberOfAdults,
                numberOfChildrenTo3: document.NumberOfChildrenTo3,
                numberOfChildrenTo10: document.NumberOfChildrenTo10,
                numberOfChildrenTo18: document.NumberOfChildrenTo18,
                tour: document.Tour,
                hotelRoom: document.HotelRoom,
                travelTo: document.TravelTo,
                travelBack: document.TravelBack,
                creationDateTime: document.CreationDateTime,
                isPromotion: document.IsPromotion,
                totalPrice: document.TotalPrice,
                version: document.Version
            );


        public static ReservationDocument AsDocument(this Reservation entity)
        {
            return new ReservationDocument
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                NumberOfAdults = entity.NumberOfAdults,
                NumberOfChildrenTo3 = entity.NumberOfChildrenTo3,
                NumberOfChildrenTo10 = entity.NumberOfChildrenTo10,
                NumberOfChildrenTo18 = entity.NumberOfChildrenTo18,
                Tour = entity.Tour,
                HotelRoom = entity.HotelRoom,
                TravelTo = entity.TravelTo,
                TravelBack = entity.TravelBack,
                IsPromotion = entity.IsPromotion,
                TotalPrice = entity.TotalPrice,
                CreationDateTime = entity.CreationDateTime,
                Version = entity.Version
            };
        }

        public static ReservationDto AsDto(this ReservationDocument document)
        {
            return new ReservationDto
            {
                Id = document.Id,
                CustomerId = document.CustomerId,
                NumberOfAdults = document.NumberOfAdults,
                NumberOfChildrenTo3 = document.NumberOfChildrenTo3,
                NumberOfChildrenTo10 = document.NumberOfChildrenTo10,
                NumberOfChildrenTo18 = document.NumberOfChildrenTo18,
                Tour = document.Tour,
                HotelRoom = document.HotelRoom,
                TravelTo = document.TravelTo,
                TravelBack = document.TravelBack,
                IsPromotion = document.IsPromotion,
                TotalPrice = document.TotalPrice,
                CreationDateTime = document.CreationDateTime,
            };
        }
    }
}
