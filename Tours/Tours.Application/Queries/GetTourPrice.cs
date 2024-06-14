using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Application.DTO;

namespace Tours.Application.Queries;

public class GetTourPrice : IQuery<double?>
{
    public Guid TourId { get; set; }
    public int NumberOfAdults { get; set; }
    public int NumberOfChildrenTo3 { get; set; }
    public int NumberOfChildrenTo10 { get; set; }
    public int NumberOfChildrenTo18 { get; set; }
    public int MealType { get; set; }
    public List<Room> Rooms { get; set; }
    public string PromotionCode { get; set; }
}

public class Room
{
    public int Amount { get; set; }

    public int Capacity { get; set; }
    public int Type { get; set; }
}