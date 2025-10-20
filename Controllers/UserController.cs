using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    [Authorize(Roles = "User")]
    [HttpGet("me")]
    public IActionResult GetCurrentUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized("User ID not found in token.");

        return Ok(new { userId });
    }
    
    [HttpGet("claims")]
    public IActionResult GetClaims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(claims);
    }

}
