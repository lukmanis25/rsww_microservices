﻿using Convey.MessageBrokers.RabbitMQ;
using Transports.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transports.Infrastructure.Exceptions
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
