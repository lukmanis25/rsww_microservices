using System;
using System.Collections.Generic;

namespace Tours.Core.Entities;

public class Tour : AggregateRoot
{
    public Tour(Guid id, DateTime startDatetime, DateTime endDatetime, string destinationPlace, string departurePlace,
        HotelResource hotelResource, TransportResource transportToResource, TransportResource transportBackResource)
    {
        Id = id;
        StartDatetime = startDatetime;
        EndDatetime = endDatetime;
        DestinationPlace = destinationPlace;
        DeparturePlace = departurePlace;
        HotelResource = hotelResource;
        TransportToResource = transportToResource;
        TransportBackResource = transportBackResource;
    }

    public DateTime StartDatetime { get; private set; }
    public DateTime EndDatetime { get; private set; }
    public string DestinationPlace { get; private set; }
    public string DeparturePlace { get; private set; }
    public HotelResource HotelResource { get; private set; }
    public TransportResource TransportToResource { get; private set; }
    public TransportResource TransportBackResource { get; private set; }
}

public class HotelResource
{
    public HotelResource(Guid documentHotelResourceId, string documentHotelName, List<Room> toList)
    {
        HotelResourceId = documentHotelResourceId;
        HotelName = documentHotelName;
        Rooms = toList;
    }

    public Guid HotelResourceId { get; set; }
    public string HotelName { get; set; }
    public List<Room> Rooms { get; set; }
}

public class Room
{
    public Room(int argCapacity, string argType, int argNumberOf, int argReservedNumber, float argPrice)
    {
        Capacity = argCapacity;
        Type = argType;
        NumberOf = argNumberOf;
        ReservedNumber = argReservedNumber;
        Price = argPrice;
    }

    public int Capacity { get; set; }
    public string Type { get; set; } // "Small" / "Medium" / "Large" / "Apartment" / "Studio"
    public int NumberOf { get; set; }
    public int ReservedNumber { get; set; }
    public float Price { get; set; }
}

public class TransportResource
{
    public TransportResource(Guid documentTransportResourceId, int documentSeatNumber, int documentReservedSeatNumber,
        float documentPrice)
    {
        TransportResourceId = documentTransportResourceId;
        SeatNumber = documentSeatNumber;
        ReservedSeatNumber = documentReservedSeatNumber;
        Price = documentPrice;
    }

    public Guid TransportResourceId { get; set; }
    public int SeatNumber { get; set; }
    public int ReservedSeatNumber { get; set; }
    public float Price { get; set; }
}