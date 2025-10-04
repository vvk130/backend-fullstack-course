using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorsesController : ControllerBase
    {
        private readonly IHorseService _horseService;
        private readonly IHorseBreedService _horseBreedService;

        public HorsesController(IHorseService horseService, IHorseBreedService horseBreedService)
        {
            _horseService = horseService;
            _horseBreedService = horseBreedService;
        }

        [HttpGet("random-name")]
        public IActionResult GetRandomHorseName()
        {
            var name = _horseService.GenerateRandomHorseName();
            return Ok(new { horseName = name });
        }

        [HttpGet("height-by-breed")]
        public IActionResult GetRandomHorseHeight([FromQuery] Breed breed)
        {
            var height = _horseBreedService.GetRandomHeightForBreed(breed);
            return Ok(new { horseHeight = height });
        }

    }

}
