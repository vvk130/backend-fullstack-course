using Microsoft.AspNetCore.Mvc;
using MassTransit;

namespace YourNamespace.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class FoalsController : ControllerBase
{
    private readonly IFoalCreationService _foalCreationService;
    private readonly AppDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public FoalsController(IFoalCreationService foalCreationService, AppDbContext context, IPublishEndpoint publishEndpoint)
    {
        _foalCreationService = foalCreationService;
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFoal([FromBody] FoalHorseRequestDto Request, ItemType type)
    {
        var result = await _foalCreationService.FoalTaskHandler(Request.SireId, Request.DamId, Request.type);

        if (!result.Success)
        {
            return ValidationProblem(new ValidationProblemDetails(result.ValidationErrors)
            {
                Title = "Foal creation failed",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var @event = new ItemCreatedEvent(
        result.Value!.Id,
        result.Value!.Name
        );

        await _publishEndpoint.Publish(@event);

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
