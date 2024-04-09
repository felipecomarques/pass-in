using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.API.Controllers;
[Route("[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(RequestEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid eventId)
    {
        var useCase = new GetEventByIdUseCase();
        var response = useCase.Execute(eventId);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterEvent([FromBody] RequestEventJson request)
    {
        var useCase = new RegisterEventUseCase();
        var response = useCase.Execute(request);
        return Created(string.Empty, response);
    }
}
