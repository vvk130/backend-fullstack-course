using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorsesController : ControllerBase
    {
        private readonly IHorseService _horseService;

        public HorsesController(IHorseService horseService)
        {
            _horseService = horseService;
        }

        [HttpGet("random-name")]
        public IActionResult GetRandomHorseName()
        {
            var name = _horseService.GenerateRandomHorseName();
            return Ok(new { horseName = name });
        }

    }

}
