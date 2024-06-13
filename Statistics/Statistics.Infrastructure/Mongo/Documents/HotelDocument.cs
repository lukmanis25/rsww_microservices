using Convey.Types;
using Statistics.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace Statistics.Infrastructure.Mongo.Documents
{
    internal sealed class HotelDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; }

    }
}
