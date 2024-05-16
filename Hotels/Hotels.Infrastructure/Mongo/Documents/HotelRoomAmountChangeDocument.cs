using Convey.Types;
using Hotels.Core.Entities;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Infrastructure.Mongo.Documents
{
    internal sealed class HotelRoomAmountChangeDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Room Room { get; set; }
    }
}
