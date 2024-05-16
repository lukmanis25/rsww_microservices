﻿    using Convey.CQRS.Events;
using Convey.MessageBrokers.RabbitMQ;
using Convey.MessageBrokers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Convey.MessageBrokers.Outbox;
using Hotels.Application.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Infrastructure.Services
{
    internal sealed class MessageBroker : IMessageBroker
    {
        private readonly IBusPublisher _busPublisher;

        public MessageBroker(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public Task PublishAsync(params IEvent[] events) => PublishAsync(events?.AsEnumerable());

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events is null)
            {
                return;
            }

            foreach (var @event in events)
            {
                if (@event is null)
                {
                    continue;
                }

                var messageId = Guid.NewGuid().ToString("N");


                await _busPublisher.PublishAsync(@event, messageId);
            }
        }
    }
}
