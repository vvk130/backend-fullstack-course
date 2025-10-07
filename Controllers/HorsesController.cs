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

        [HttpGet("random-height-for-breed")]
        public IActionResult GetRandomHorseHeight([FromQuery] Breed breed)
        {
            var height = _horseBreedService.GetRandomHeightForBreed(breed);
            return Ok(new { horseHeight = height });
        }

        [HttpGet("random-color-for-breed")]
        public IActionResult GetRandomHorseColor([FromQuery] Breed breed)
        {
            var color = _horseBreedService.GetRandomColorForBreed(breed);
            return Ok(new { horseColor = color });
        }

        //for developement purposes
        [HttpGet("all-horses")]
        public IActionResult GetAll()
        {
            var horses = _horseService.GetAll();

            return Ok(new { horses }); 
        }

        [HttpPost("create-horse")]
        public IActionResult CreateHorse()
        {
            var horse = _horseService.CreateHorse();
            return Ok(new { horse }); 
        }
    }

}
