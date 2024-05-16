using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using Hotels.Core.Entities;
using Hotels.Core.Repositories;
using Hotels.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.Core.Events;
using Reservations.Infrastructure.Mongo.Documents;
using Reservations.Core.ValueObjects;

namespace Hotels.Infrastructure.Mongo.Repositories
{
    internal sealed class HotelEventMongoRepository : IHotelEventRepository
    {
        private readonly IMongoRepository<HotelRoomAmountChangeDocument, Guid> _repository;
        private readonly IMongoDatabase _database;

        public HotelEventMongoRepository(IMongoRepository<HotelRoomAmountChangeDocument, Guid> repository, IMongoDatabase database)
        {
            _repository = repository;
            _database = database;
        }
            

        public Task AddEvent(HotelRoomAmountChange @event) =>
            _repository.AddAsync(@event.AsDocument());
    

        public async Task<HotelResource> GetHotelResource(AggregateId hotelId)
        {
            var events = await _database.GetCollection<HotelRoomAmountChangeDocument>("hotel_events")
                .Find(r => r.Id == hotelId)
                .ToListAsync();

            List<Room> roomList = [];

            foreach (var @event in events)
            {
                var existingRoom = roomList.FirstOrDefault(room => room == @event.Room);
                if (existingRoom != null)
                {
                    existingRoom.Amount += @event.Room.Amount;
                }
                else
                {
                    roomList.Add(@event.Room);
                }
            }
            return new HotelResource(hotelId, roomList);
        }
    }
}
