using Convey.MessageBrokers.RabbitMQ;
using System;

namespace Tours.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                //tu powinno być wyłapywanie exceptionów i rzucanie eventów kompensujących
                //HotelRoomCancelReservationException ex => new ReservateOffertRejected(ex., ex.Message,
                //    ex.Code)
            };
    }
}