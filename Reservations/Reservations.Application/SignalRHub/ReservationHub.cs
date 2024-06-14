using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Reservations.Application.SignalRHub
{
    public class ReservationHub : Hub<INotificationsClient>
    {
        public async Task NotifyReservation(string tourId, string type)
        {
            await Clients.All.ReceiveNotification($"ReceiveReservationEvent {tourId} {type}");
        }
    }
}
