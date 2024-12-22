using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events;
using PassIn.Application.UseCases.Events.GetAll;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
public class EventsController : ControllerBase
{


    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterJsonEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request, [FromServices] RegisterEventUseCase useCase)
    {
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id, [FromServices] GetEventByIdUseCase useCase)
    {

            var response = useCase.Execute(id);

            return Ok(response);    
    }

    [HttpPost]
    [Route("{eventId}/attendee")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult RegisterAttendee(
                [FromBody] RequestRegisterAttendee request, 
                [FromRoute] Guid eventId, 
                [FromServices] RegisterAttendeeUseCase useCase )
    {
        var response = useCase.Execute(request, eventId);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<ResponseEventJson>), StatusCodes.Status200OK)]
    public IActionResult GetAll([FromServices] GetAllEventsUseCase useCase)
    {
        var response = useCase.Execute();
        return Ok(response);
    }
}
