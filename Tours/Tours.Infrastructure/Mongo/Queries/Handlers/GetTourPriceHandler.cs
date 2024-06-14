using Convey.CQRS.Queries;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tours.Application;
using Tours.Application.DTO;
using Tours.Application.Queries;
using Tours.Infrastructure.Mongo.Documents;

namespace Tours.Infrastructure.Mongo.Queries.Handlers;

//public class GetTourPrice : IQuery<double?>
//{
//    public Guid TourId { get; set; }
//    public int NumberOfAdults { get; set; }
//    public int NumberOfChildrenTo3 { get; set; }
//    public int NumberOfChildrenTo10 { get; set; }
//    public int NumberOfChildrenTo18 { get; set; }
//    public int MealType { get; set; }
//    public List<Room> Rooms { get; set; }
//    public string PromotionCode { get; set; }
//}

//public class Room
//{
//    public int Capacity { get; set; }
//    public string Type { get; set; }
//    public int Amount { get; set; }
//}

internal sealed class GetTourPriceHandler : IQueryHandler<GetTourPrice, double?>
{
    private readonly IMongoDatabase _database;

    public GetTourPriceHandler(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task<double?> HandleAsync(GetTourPrice query, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<TourDocument>("tours");
        var tour = await collection.Find(d => d.Id == query.TourId).FirstOrDefaultAsync(cancellationToken);
        if (tour != null)
        {
            double price = 0.0;

            var priceTransportTo = tour.TransportToResource.Price;
            var priceTransportFrom = tour.TransportBackResource.Price;
            var fullBoardPrice = priceTransportTo + priceTransportFrom;

            price += query.NumberOfAdults * fullBoardPrice;
            price += query.NumberOfChildrenTo3 * fullBoardPrice * 0.2;
            price += query.NumberOfChildrenTo10 * fullBoardPrice * 0.5;
            price += query.NumberOfChildrenTo18 * fullBoardPrice * 0.8;

            foreach (var room in query.Rooms)
            {
                // find room by type and capacity
                var roomType = tour.HotelResource.Rooms.FirstOrDefault(r => r.Capacity == room.Capacity && (int)r.Type.RoomTypeFromString() == room.Type);
                if (roomType != null)
                {
                    price += room.Amount * roomType.Price;
                }
            }


            if (query.MealType == 1)
            {
                var numberOfDays = (tour.EndDatetime - tour.StartDatetime).Days;
                price += 300 * numberOfDays;
            }


            // if promotion code is equal to "PROMO" then apply 50% discount
            if (query.PromotionCode == "PROMO")
            {
                price *= 0.5;
            }

            return price;
        }

        return null;
    }
}