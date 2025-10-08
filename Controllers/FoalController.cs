using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class FoalsController : ControllerBase
{
    private readonly IFoalCreationService _foalCreationService;
    private readonly AppDbContext _context;

    public FoalsController(IFoalCreationService foalCreationService, AppDbContext context)
    {
        _foalCreationService = foalCreationService;
       _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFoal([FromBody] FoalHorseRequestDto Request)
    {
        var result = await _foalCreationService.FoalTaskHandler(Request.Sire, Request.Dam);

        if (!result.Success)
        {
            return ValidationProblem(new ValidationProblemDetails(result.ValidationErrors)
            {
                Title = "Foal creation failed",
                Status = StatusCodes.Status400BadRequest
            });
        }

        return CreatedAtAction(
        nameof(GetFoal),
        new { id = result.Value!.Id },
        result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFoal(Guid id)
    {
        var foal = await _context.Horses.FindAsync(id);
        if (foal == null)
            return NotFound();

        return Ok(foal);
    }

}
}
