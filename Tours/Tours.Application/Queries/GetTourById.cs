using Convey.CQRS.Queries;
using Tours.Application.DTO;

namespace Tours.Application.Queries;

public class GetTourById : IQuery<TourDto>
{
    public Guid Id { get; set; }
}