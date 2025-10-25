using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/CompStatistics")]
[ApiController]
public class CompStatisticsController : ControllerBase
{
    private readonly ICompStatisticsService _compStatisticsService;

    public CompStatisticsController(ICompStatisticsService compStatisticsService) 
    { 
        _compStatisticsService = compStatisticsService;
    }

    [HttpGet("statistics")]
    public async  Task<IActionResult> GetStatisticsById(Guid HorseId)
    {
        var statistics = await _compStatisticsService.CreateCompResultStatistics(HorseId);

        return Ok(statistics);
    }

    [HttpGet("statistics-paginated")]
    public async Task<IActionResult> GetStatisticsById([FromQuery] PaginationRequest request)
    {
        var result = await _compStatisticsService.GetPaginatedAsync(request);
        return Ok(result);
    }

}