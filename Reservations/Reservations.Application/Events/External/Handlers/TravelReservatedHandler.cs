using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External.Handlers
{
    public class TravelReservatedHandler : IEventHandler<TravelReservated>
    {
        public Task HandleAsync(TravelReservated @event, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
