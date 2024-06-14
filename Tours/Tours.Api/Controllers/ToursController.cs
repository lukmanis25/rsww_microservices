using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using Tours.Application.Queries;

namespace Tours.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ToursController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public ToursController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{tourId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [SwaggerOperation(
        Summary = "Get a tour by its ID",
        Description = "Returns a specific tour based on the provided tour ID."
    )]
    public async Task<IActionResult> GetTourById([FromRoute] Guid tourId)
    {
        var result = await _queryDispatcher.QueryAsync(new GetTourById { Id = tourId });
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [SwaggerOperation(
        Summary = "Get tours based on filters",
        Description = "Returns a list of tours based on specified filters like destination, departure dates, etc."
    )]
    public async Task<IActionResult> GetTours([FromQuery] GetTours queryParameters)
    {
        // Check dates
        // if start is null, set to today
        if (queryParameters.StartDate is null)
        {
            queryParameters.StartDate = DateTime.Now;
        }

        // if end is null, set to 1 year from now
        if (queryParameters.EndDate is null)
        {
            queryParameters.EndDate = DateTime.Now.AddYears(1);
        }

        // if adults is null, set to 0
        if (queryParameters.Adults is null)
        {
            queryParameters.Adults = 0;
        }

        // if children is null, set to 0
        if (queryParameters.Children is null)
        {
            queryParameters.Children = 0;
        }

        var result = await _queryDispatcher.QueryAsync(queryParameters);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("{tourId}/price")]
    public async Task<IActionResult> GetTourPrice([FromBody] GetTourPrice query)
    {   
        // get tourId from route
        query.TourId = Guid.Parse(HttpContext.Request.RouteValues["tourId"].ToString());
        var result = await _queryDispatcher.QueryAsync(query);
        return result is null ? NotFound() : Ok(result);
    }

}
