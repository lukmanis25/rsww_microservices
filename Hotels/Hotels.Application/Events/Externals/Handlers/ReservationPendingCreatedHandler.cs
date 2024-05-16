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
    public class ReservationPendingCreatedHandler : IEventHandler<ReservationPendingCreated>
    {
        private readonly IHotelEventRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ReservationPendingCreatedHandler(IHotelEventRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReservationPendingCreated @event, CancellationToken cancellationToken = default)
        {
            var hotelResource = await _repository.GetHotelResource(@event.HotelRoom.HotelId);         

            foreach(var room in @event.HotelRoom.Rooms)
            {   
                await _repository.AddEvent(new Core.Events.HotelRoomAmountChange
                {
                    HotelId = @event.HotelRoom.HotelId,
                    Room = new Reservations.Core.ValueObjects.Room
                    {
                        Amount = room.Amount * (-1),
                        Capacity = room.Capacity,
                        Type = room.Type
                    } ,
                });

                var hotelResourceRoomAmount = hotelResource != null ? hotelResource.GetRoomAmout(room) : 0;
                await _messageBroker.PublishAsync(new HotelAvailabilityChanged
                {
                    HotelId = @event.HotelRoom.HotelId,
                    Amount = hotelResourceRoomAmount - room.Amount,
                    Type = room.Type,
                    Capacity = room.Capacity
                });
            }
            if (hotelResource == null || !hotelResource.IfRoomsCanBeReserved(@event.HotelRoom.Rooms))
            {
                await _messageBroker.PublishAsync(new HotelReservationRejected
                {
                    HotelId = @event.HotelRoom.HotelId,
                    ReservationId = @event.ReservationId
                });
                return;
            }
            else
            {
                await _messageBroker.PublishAsync(new HotelReserved
                {
                    HotelId = @event.HotelRoom.HotelId,
                    ReservationId = @event.ReservationId
                });
            }
            
        }
    }
}
