using Convey.CQRS.Commands;
using Reservations.Application.Events;
using Reservations.Application.Exceptions;
using Reservations.Application.Services;
using Reservations.Core.Entities;
using Reservations.Core.Repositories;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Commands.Handlers
{
    public class AddReservationHandler : ICommandHandler<AddReservation>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public AddReservationHandler(IReservationRepository repository, IMessageBroker messageBroker )
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(AddReservation command, CancellationToken cancellationToken = default)
        {
            var reservation = Reservation.Create(
                id: command.Id,
                customerId: command.CustomerId,
                numberOfAdults: command.NumberOfAdults,
                numberOfChildrenTo3: command.NumberOfChildrenTo3,
                numberOfChildrenTo10: command.NumberOfChildrenTo10,
                numberOfChildrenTo18: command.NumberOfChildrenTo18,
                tour: command.Tour,
                hotelId: command.HotelId,
                mealType: command.MealType,
                rooms: command.Rooms,
                hotelRoomPrice: command.HotelRoomPrice,
                travelToId: command.TravelToId,
                travelToPrice: command.TravelToPrice,
                travelBackId: command.TravelBackId,
                travelBackPrice: command.TravelBackPrice,
                promotionCode: command.PromotionCode
                );
            await _repository.AddAsync(reservation);
            await _messageBroker.PublishAsync(new ReservationPendingCreated { 
                ReservationId = reservation.Id,
                HotelRoom = new HotelRoomEventDto { HotelId = command.HotelId, Rooms = reservation.HotelRoom.Rooms },
                TravelTo = new TransportEventDto { TransportId = reservation.TravelTo.ResourceId },
                TravelBack = new TransportEventDto { TransportId = reservation.TravelBack.ResourceId },
                NumberOfPeople = reservation.NumberOfAdults + reservation.NumberOfChildrenTo3 
                + reservation.NumberOfChildrenTo10 + reservation.NumberOfChildrenTo18
                });

            //TEST - przeniesc potem do obbsługi eventu 
            await _messageBroker.PublishAsync(new ReservationPurchasePending
            {
                ReservationId = reservation.Id,
                CustomerId = reservation.CustomerId,
                ReservedUntil = reservation.CreationDateTime.AddMinutes(1),
                TotalPrice = reservation.TravelTo.Price + reservation.TravelBack.Price + reservation.HotelRoom.Price,
            });

            StartReservationTimeCounting(reservation.Id);

        }

        private async Task StartReservationTimeCounting(AggregateId reservationId)
        {
            await Task.Delay(TimeSpan.FromSeconds(60));

            //obsługa tego że kilka wątków może coś robić z bazą, ale ostatecznie liczy się wersja agregatu 
            var attemps = 3;
            while(attemps > 0)
            {
                var reservation = await _repository.GetAsync(reservationId);
                if (
                    (reservation.TravelBack.Status == ReservationStatus.Purchased || reservation.TravelBack.Status == ReservationStatus.Cancelled) &&
                    (reservation.TravelTo.Status == ReservationStatus.Purchased || reservation.TravelTo.Status == ReservationStatus.Cancelled) &&
                    (reservation.HotelRoom.Status == ReservationStatus.Purchased || reservation.HotelRoom.Status == ReservationStatus.Cancelled)
                )
                {
                    //jak juz wcześniej zostało anulowane lub jest już zakupione to nic nie zmieniamy
                    return;
                }

                reservation.CancelReservation();
                var ifSucceed = await _repository.UpdateAsync(reservation);
                if (ifSucceed)
                {
                    //jak się udało zanulować to wysyłamy event i koniec
                    await _messageBroker.PublishAsync(new ReservationCancelled
                    {
                        ReservationId = reservationId,
                        TravelTo = new TransportEventDto { TransportId = reservation.TravelTo.ResourceId },
                        TravelBack = new TransportEventDto { TransportId = reservation.TravelBack.ResourceId },
                        HotelRoom = new HotelRoomEventDto
                        {
                            HotelId = reservation.HotelRoom.ResourceId,
                            Rooms = reservation.HotelRoom.Rooms,
                        },
                        NumberOfPeople = reservation.GetNumberOfPeople()
                    });
                    return;
                }
                //jak się nie udało to próbujemy jeszcze raz za 1s
                await Task.Delay(TimeSpan.FromSeconds(1));
                attemps--;
            }
        }
    }
}
