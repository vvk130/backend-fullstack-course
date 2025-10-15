using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

[ApiController]
[Route("api/[controller]")]
public class PuzzleController : GenericController<PuzzleAnswer, PuzzleAnswerShortDto>
{
    private readonly IPuzzleService _puzzleService;

    public PuzzleController(IPuzzleService puzzleService, IGenericService<PuzzleAnswer> genericService): base(genericService) 
    {
        _puzzleService = puzzleService;
    }

    [HttpGet("check-all-pieces")]
    public async Task<IActionResult> CheckAllPieces([FromBody] PuzzleCorrectionRequest request)
    {
        //request.id check!
        
        var isCorrect = await _puzzleService.CheckAllPieces(request);

        if (isCorrect)
        {
            return Ok("All pieces are in the correct positions.");
        }

        return BadRequest("One or more pieces are in the wrong position.");
    }
}

