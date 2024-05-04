using Convey.MessageBrokers.RabbitMQ;
using Reservations.Application.Events.Rejected;
using Reservations.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                //HotelRoomsOccupancyExceededException ex => new ReservateOffertRejected(ex., ex.Message,
                //    ex.Code)
            };
    }
}
