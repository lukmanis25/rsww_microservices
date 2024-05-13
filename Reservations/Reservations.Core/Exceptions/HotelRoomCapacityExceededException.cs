using System;
namespace Reservations.Core.Exceptions
{
    public class HotelRoomCapacityExceededException : DomainException
    {
        public override string Code { get; } = "hotel_room_capacity_exceeded";

        public HotelRoomCapacityExceededException(int numberOfPeople, int roomCapacity) 
            : base($"Hotel room capacity exceeded. Rooms capacity: {roomCapacity}, Number of people: {numberOfPeople} ") { }
    }
}
