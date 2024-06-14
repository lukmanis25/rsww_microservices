using System.Linq;
using Tours.Application.DTO;
using Tours.Core.Entities;

namespace Tours.Infrastructure.Mongo.Documents;

internal static class Extensions
{
    public static HotelResource AsEntity(this HotelResourceDocument document)
    {
        return new HotelResource(
            document.HotelResourceId,
            document.HotelName,
            document.Rooms.Select(r => new Room(r.Capacity, r.Type, r.NumberOf, r.ReservedNumber, r.Price)).ToList());
    }

    public static TransportResource AsEntity(this TransportResourceDocument document)
    {
        return new TransportResource(
            document.TransportResourceId,
            document.SeatNumber,
            document.ReservedSeatNumber,
            document.Price);
    }

    public static HotelResourceDocument AsDocument(this HotelResource entity)
    {
        return new HotelResourceDocument
        {
            HotelResourceId = entity.HotelResourceId,
            HotelName = entity.HotelName,
            Rooms = entity.Rooms.Select(r => new RoomDocument
            {
                Capacity = r.Capacity,
                Type = r.Type,
                NumberOf = r.NumberOf,
                ReservedNumber = r.ReservedNumber,
                Price = r.Price
            }).ToList()
        };
    }

    public static TransportResourceDocument AsDocument(this TransportResource entity)
    {
        return new TransportResourceDocument
        {
            TransportResourceId = entity.TransportResourceId,
            SeatNumber = entity.SeatNumber,
            ReservedSeatNumber = entity.ReservedSeatNumber,
            Price = entity.Price
        };
    }

    public static Tour AsEntity(this TourDocument document)
    {
        return new Tour(
            document.Id,
            document.StartDatetime,
            document.EndDatetime,
            document.DestinationPlace,
            document.DeparturePlace,
            document.HotelResource.AsEntity(),
            document.TransportToResource.AsEntity(),
            document.TransportBackResource.AsEntity());
    }

    public static TourDocument AsDocument(this Tour entity)
    {
        return new TourDocument
        {
            Id = entity.Id,
            StartDatetime = entity.StartDatetime,
            EndDatetime = entity.EndDatetime,
            DestinationPlace = entity.DestinationPlace,
            DeparturePlace = entity.DeparturePlace,
            HotelResource = entity.HotelResource.AsDocument(),
            TransportToResource = entity.TransportToResource.AsDocument(),
            TransportBackResource = entity.TransportBackResource.AsDocument()
        };
    }

    public static HotelResourceDto AsDto(this HotelResourceDocument document)
    {
        return new HotelResourceDto
        {
            HotelResourceId = document.HotelResourceId,
            HotelName = document.HotelName,
            Rooms = document.Rooms.Select(r => new RoomDto
            {
                Capacity = r.Capacity,
                Type = r.Type,
                NumberOf = r.NumberOf,
                ReservedNumber = r.ReservedNumber,
                Price = r.Price
            }).ToList()
        };
    }

    public static TransportResourceDto AsDto(this TransportResourceDocument document)
    {
        return new TransportResourceDto
        {
            TransportResourceId = document.TransportResourceId,
            SeatNumber = document.SeatNumber,
            ReservedSeatNumber = document.ReservedSeatNumber,
            Price = document.Price
        };
    }


    public static TourDto AsDto(this TourDocument document)
    {
        return new TourDto
        {
            Id = document.Id,
            StartDatetime = document.StartDatetime,
            EndDatetime = document.EndDatetime,
            DestinationPlace = document.DestinationPlace,
            DeparturePlace = document.DeparturePlace,
            HotelResource = document.HotelResource.AsDto(),
            TransportToResource = document.TransportToResource.AsDto(),
            TransportBackResource = document.TransportBackResource.AsDto()
        };
    }
}