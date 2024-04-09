using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.DoAttendeesCheckIn;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.API.Controllers;
[Route("[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(RequestEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult Checkin([FromRoute] Guid attendeeId)
    {
        var useCase = new DoAttendeesCheckInUseCase();
        var response = useCase.Execute(attendeeId);
        return Created(string.Empty, response);
    }
}
