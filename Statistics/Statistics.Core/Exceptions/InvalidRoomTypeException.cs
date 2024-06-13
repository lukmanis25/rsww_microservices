using System;

namespace Statistics.Core.Exceptions
{
    public class InvalidRoomTypeException : DomainException
    {
        public override string Code { get; } = "invalid_room_type";

        public InvalidRoomTypeException() : base($"Invalid room type") { }
    }
}
