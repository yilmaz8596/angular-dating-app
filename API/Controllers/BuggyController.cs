

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("auth")]

    public IActionResult GetAuth()
    {
        return Unauthorized("Unauthorized");
    }

    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        return NotFound("Not Found");
    }

    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
        throw new Exception("Server Error");
    }


    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("Bad Request");
    }

}