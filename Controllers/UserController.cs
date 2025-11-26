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

        [HttpPost("wallet-by-username")]
        public async Task<IActionResult> GetOrCreateWalletByUsername([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Username is required.");

            var user = await _context.Users
                .Where(u => u.UserName == username)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("User not found.");

            var ownerId = Guid.Parse(user.Id);

            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(w => w.OwnerId == ownerId);

            if (wallet == null)
            {
                wallet = new Wallet
                {
                    OwnerId = ownerId,
                    Balance = 75000
                };

                _context.Wallets.Add(wallet);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                wallet.Id,
                wallet.OwnerId,
                wallet.Balance
            });
        }

}
