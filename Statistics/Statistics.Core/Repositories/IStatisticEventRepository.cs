using Statistics.Core.Events;
using Statistics.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Core.Repositories
{
    public interface IStatisticEventRepository
    {
        Task AddEvent(StatisticAmountChange @event);

    }
}