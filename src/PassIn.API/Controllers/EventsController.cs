﻿using Microsoft.AspNetCore.Mvc;
using PassIn.Communication.Requests;

namespace PassIn.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        return Created();
    }
}
