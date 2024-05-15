using Convey.CQRS.Commands;
using Reservations.Core.Entities;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Commands
{
    public class CancelReservation : ICommand
    {
        public Guid ReservationId { get; set; }
    }
}
