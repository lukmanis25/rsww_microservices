using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using Tours.Application.DTO;
using Tours.Application.Queries;
using Tours.Infrastructure.Mongo.Documents;

namespace Tours.Infrastructure.Mongo.Queries.Handlers
{
    internal sealed class GetToursHandler : IQueryHandler<GetTours, IEnumerable<TourDto>>
    {
        private readonly IMongoDatabase _database;

        public GetToursHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<TourDto>> HandleAsync(GetTours query, CancellationToken cancellationToken = default)
        {
            var capacity = query.Adults + query.Children;

            var combinedFilter = (Expression<Func<TourDocument, bool>>)(
                t => (string.IsNullOrEmpty(query.Destination) || t.DestinationPlace == query.Destination)
                     && (string.IsNullOrEmpty(query.Departure) || t.DeparturePlace == query.Departure)
                     && t.StartDatetime >= query.StartDate
                     && t.EndDatetime <= query.EndDate
            );


            try
            {
                var documents = await _database.GetCollection<TourDocument>("tours")
                .Find(combinedFilter)
                .ToListAsync();

                var filtered = documents.Where(d => d.TransportToResource.ReservedSeatNumber + capacity <= d.TransportToResource.SeatNumber
                                                && d.TransportBackResource.ReservedSeatNumber + capacity <= d.TransportBackResource.SeatNumber);

                filtered = filtered.Where(d => HasSufficientRoomCapacity(d.HotelResource.Rooms, capacity));

                return filtered.Select(d => d.AsDto());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return null;
        }

        private bool HasSufficientRoomCapacity(IEnumerable<RoomDocument> rooms, int? requiredCapacity)
        {
            List<int> availableCapacities = new List<int>();

            foreach (var room in rooms)
            {
                int availableRooms = room.NumberOf - room.ReservedNumber;
                for (int i = 0; i < availableRooms; i++)
                {
                    availableCapacities.Add(room.Capacity);
                }
            }

            return CanMeetCapacity(availableCapacities, requiredCapacity);
        }

        private bool CanMeetCapacity(List<int> capacities, int? requiredCapacity)
        {
            return CheckCombination(capacities, 0, 0, requiredCapacity);
        }

        private bool CheckCombination(List<int> capacities, int startIndex, int currentSum, int? requiredCapacity)
        {
            if (currentSum >= requiredCapacity)
            {
                return true;
            }

            for (int i = startIndex; i < capacities.Count; i++)
            {
                if (CheckCombination(capacities, i + 1, currentSum + capacities[i], requiredCapacity))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
