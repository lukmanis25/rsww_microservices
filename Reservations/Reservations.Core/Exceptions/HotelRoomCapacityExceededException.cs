using System;
namespace Reservations.Core.Exceptions
{
    public class HotelRoomCapacityExceededException : DomainException
    {
        public override string Code { get; } = "hotel_room_capacity_exceeded";

        public HotelRoomCapacityExceededException(int numberOfPeople, int roomOccupancy) 
            : base($"Hotel room capacity exceeded. Room occupancy: {roomOccupancy}, Number of people: {numberOfPeople} ") { }
    }
}
