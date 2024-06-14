using System.Globalization;
using Convey.CQRS.Queries;
using Tours.Application.DTO;

namespace Tours.Application.Queries;

public class GetTours : IQuery<IEnumerable<TourDto>>
{
    public string? Destination { get; set; }
    public string? Departure { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Adults { get; set; }
    public int? Children { get; set; }

    // Empty constructor for serialization
    public GetTours()
    {
    }

    // Constructor with parameters for creating queries
    public GetTours(string? destination, string? departure, DateTime? startDate, DateTime? endDate, int? adults, int? children)
    {
        Destination = destination;
        Departure = departure;
        StartDate = startDate;
        EndDate = endDate;
        Adults = adults;
        Children = children;
    }
}
