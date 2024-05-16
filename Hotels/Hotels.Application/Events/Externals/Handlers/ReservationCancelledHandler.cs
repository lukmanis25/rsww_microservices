using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Hotels.Application.Services;
using Hotels.Core.Entities;
using Hotels.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotels.Application.Events
{
    public class ReservationCancelledHandler : IEventHandler<ReservationCancelled>
    {
        private readonly IHotelEventRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ReservationCancelledHandler(IHotelEventRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReservationCancelled @event, CancellationToken cancellationToken = default)
        {
            foreach(var room in @event.HotelRoom.Rooms)
            {
                await _repository.AddEvent(new Core.Events.HotelRoomAmountChange
                {
                    HotelId = @event.HotelRoom.HotelId,
                    Room = room
                });

                await _messageBroker.PublishAsync(new HotelAvailabilityChanged
                {
                    HotelId = @event.HotelRoom.HotelId,
                    Amount = room.Amount,
                    Type = room.Type,
                    Capacity = room.Capacity
                });
            }
        }
    }
}
