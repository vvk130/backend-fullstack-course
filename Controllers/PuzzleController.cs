[ApiController]
[Route("api/[controller]")]
public class PuzzleController : GenericController<PuzzleAnswer, PuzzleAnswerShortDto>
{
    private readonly IPuzzleService _puzzleService;

    public PuzzleController(IPuzzleService puzzleService)
    {
        _puzzleService = puzzleService;
    }

    //TODO create puzzle


    [HttpGet("check-all-pieces")]
    public IActionResult CheckAllPieces([FromBody] PuzzleCorrectionRequest request)
    {
        //request.id check!
        
        var isCorrect = _puzzleService.CheckAllPieces(request);

        if (isCorrect)
        {
            return Ok("All pieces are in the correct positions.");
        }

        return BadRequest("One or more pieces are in the wrong position.");
    }
}

