using System;

namespace Reservations.Core.Exceptions
{
    public class InvalidMealTypeException : DomainException
    {
        public override string Code { get; } = "invalid_meal_type";

        public InvalidMealTypeException() : base($"Invalid meal type") { }
    }
}
