using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("by-username")]
        public IActionResult GetUserIdByUsername([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Username is required.");

            var user = _context.Users
                .Where(u => u.Email == username)
                .Select(u => new { u.Id })
                .FirstOrDefault();

            if (user == null)
                return NotFound("User not found.");

            return Ok(user); 
        }

        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(claims);
        }

}
