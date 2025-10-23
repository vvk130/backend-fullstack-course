using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/stablecleaning")]
[ApiController]
public class StableController : ControllerBase
{
    private readonly AppDbContext _context;

    public StableController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPut("clean-stable-pay-user")]
    public async Task<IActionResult> CleanStablePayUser(Guid id)
    {
        var rowsUpdated = await _context.Wallets
            .Where(w => w.OwnerId == id)
            .ExecuteUpdateAsync(w =>
                w.SetProperty(w => w.Balance, w => w.Balance + 1000));

        if (rowsUpdated == 0)
            return NotFound(new { message = $"No wallet found for user {id}" });

        return Ok(new { userId = id });
    }

}