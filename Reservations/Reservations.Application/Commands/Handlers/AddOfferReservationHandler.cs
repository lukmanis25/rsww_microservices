using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Commands.Handlers
{
    public class AddOfferReservationHandler : ICommandHandler<AddOfferReservation>
    {
        public Task HandleAsync(AddOfferReservation command, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
