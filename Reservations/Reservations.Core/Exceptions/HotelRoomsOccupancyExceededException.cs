using System;
namespace Reservations.Core.Exceptions
{
    public class HotelRoomsOccupancyExceededException : DomainException
    {
        public override string Code { get; } = "hotel_rooms_occupancy_exceeded";

        public HotelRoomsOccupancyExceededException(int numberOfPeople, int roomsOccupancy) 
            : base($"Hotel rooms occupancy exceeded. Rooms occupancy: {roomsOccupancy}, Number of people: {numberOfPeople} ") { }
    }
}
