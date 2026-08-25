using Ixp.Interview.Api.Middleware;
using Ixp.Interview.Api.Models;
using Ixp.Interview.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ixp.Interview.Api.Controllers;

[ApiController]
[Route("v1/faults")]
public sealed class FaultsController(IFaultService faults) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(FaultListResponse), StatusCodes.Status200OK)]
    public ActionResult<FaultListResponse> GetFaults(
        [FromQuery] string? location,
        [FromQuery] string? from,
        [FromQuery] string? to,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var correlationId = HttpContext.Items[CorrelationIdMiddleware.ItemKey]?.ToString() ?? string.Empty;
        return Ok(faults.GetFaults(location, from, to, page, pageSize, correlationId));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Fault), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Fault> GetFault(Guid id)
    {
        var fault = faults.GetFault(id);
        return fault is null ? NotFound() : Ok(fault);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Fault), StatusCodes.Status201Created)]
    public ActionResult<Fault> CreateFault([FromBody] CreateFaultRequest request)
    {
        var created = faults.CreateFault(request);
        return CreatedAtAction(nameof(GetFault), new { id = created.Id }, created);
    }
}
