using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAll;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendeesController : ControllerBase
{
    [HttpGet]
    [Route("events/{eventId}/attendees")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseAllAttendeesByEventIdJson), StatusCodes.Status200OK)]
    public IActionResult GetAll(
            [FromRoute] Guid eventId, 
            [FromServices] GetAllAttendeesByEventIdUseCase useCase)
    {
        var response = useCase.Execute(eventId);
        return Ok(response);
    }
}
