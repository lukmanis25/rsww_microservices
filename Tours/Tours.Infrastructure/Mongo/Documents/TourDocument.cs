using System;
using System.Collections.Generic;
using Convey.Types;

namespace Tours.Infrastructure.Mongo.Documents;

internal sealed class TourDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
    public string DestinationPlace { get; set; }
    public string DeparturePlace { get; set; }
    public HotelResourceDocument HotelResource { get; set; }
    public TransportResourceDocument TransportToResource { get; set; }
    public TransportResourceDocument TransportBackResource { get; set; }
}

public class HotelResourceDocument
{
    public Guid HotelResourceId { get; set; }
    public string HotelName { get; set; }
    public List<RoomDocument> Rooms { get; set; }
}

public class RoomDocument
{
    public int Capacity { get; set; }
    public string Type { get; set; }
    public int NumberOf { get; set; }
    public int ReservedNumber { get; set; }
    public float Price { get; set; }
}

public class TransportResourceDocument
{
    public Guid TransportResourceId { get; set; }
    public int SeatNumber { get; set; }
    public int ReservedSeatNumber { get; set; }
    public float Price { get; set; }
}