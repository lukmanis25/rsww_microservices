using Transports.Core.Entities;
using Transports.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transports.Core.Repositories
{
    public interface ITransportEventRepository
    {
        Task AddEvent(TransportAmountChange @event);
        Task<TransportResource> GetTransportResource(AggregateId transportId);
    }
}