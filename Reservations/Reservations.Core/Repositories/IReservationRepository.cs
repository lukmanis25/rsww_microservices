﻿using Reservations.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> GetAsync(AggregateId id);
        Task AddAsync(Reservation reservation);
        Task<bool> UpdateAsync(Reservation purchase);
        Task DeleteAsync(AggregateId id);
    }
}
