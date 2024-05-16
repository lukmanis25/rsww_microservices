using Hotels.Core.Entities;
using Hotels.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Core.Repositories
{
    public interface IHotelEventRepository
    {
        Task AddEvent(HotelRoomAmountChange @event);
        Task<HotelResource> GetHotelResource(AggregateId hotelId);
    }
}