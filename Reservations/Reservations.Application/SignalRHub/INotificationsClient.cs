using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.SignalRHub
{
    public interface INotificationsClient
    {
        Task ReceiveNotification(string content);
    }
}
