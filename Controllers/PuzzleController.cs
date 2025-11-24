using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class PuzzleController : GenericController<PuzzleAnswer, PuzzleAnswerCreateDto, PuzzleAnswerShortDto>
{
    private readonly IPuzzleService _puzzleService;
    private readonly IMapper _mapper;

    public PuzzleController(IPuzzleService puzzleService, IGenericService<PuzzleAnswer> genericService, IMapper mapper): base(genericService, mapper) 
    {
        _puzzleService = puzzleService;
    }

    [HttpPost("generate-puzzle")]
    public async Task<IActionResult> CreatePuzzle([FromBody] PuzzleRequestDto request)
    {
        var result = await _puzzleService.PuzzleGenerator(request.ImgUrl);

        if (!result.Success)
            return BadRequest(result.ValidationErrors);

        return Ok(result.Value);
    }

    [HttpPost("check-all-pieces")]
    public async Task<IActionResult> CheckAllPieces([FromBody] PuzzleCorrectionRequest request)
    {

        var isCorrect = await _puzzleService.CheckAllPieces(request);

        if (isCorrect)
            return Ok("All pieces are in the correct positions.");

        return BadRequest("One or more pieces are in the wrong position.");
    }
}

