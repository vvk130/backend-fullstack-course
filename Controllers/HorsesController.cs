using Microsoft.AspNetCore.Mvc;
using Hangfire;
using GameModel;
using System.Linq.Expressions;

namespace YourNamespace.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class HorsesController : GenericController<Horse>
{
        private readonly IHorseService _horseService;
        private readonly IHorseBreedService _horseBreedService;
        private readonly IGenericService<Horse> _genericService;

        public HorsesController(
            IHorseService horseService,
            IHorseBreedService horseBreedService,
            IGenericService<Horse> genericService)
            : base(genericService) 
        {
            _horseService = horseService;
            _horseBreedService = horseBreedService;
            _genericService = genericService;
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
        [Authorize]
        public IActionResult CreateHorse()
        {
            var horse = _horseService.CreateHorse();
            return Ok(new { horse }); 
        }

        // [HttpPatch("update-horses-energy")]
        // public IActionResult EnergyUpdateHorses()
        // {
        //     BackgroundJob.Enqueue<IHorseService>(s => s.BatchHorsesAgeUpdate());
        //     return Ok(new { message = "Horses energy updated" }); //Update not sure if update ran?
        // }

        // [HttpPatch("update-horses-age")]
        // public IActionResult AgeUpdateHorses()
        // {
        //     BackgroundJob.Enqueue<IHorseService>(s => s.BatchHorsesAgeUpdate());
        //     return Ok(new { message = "Horses age updated" }); //Update not sure if update ran?
        // }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] FileUploadRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _horseService.UploadImageAsync(request.File);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchHorses([FromQuery] HorseFilterDto filter)
        {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            Expression<Func<Horse, bool>> predicate = h =>
                (filter.Genders == null || filter.Genders.Contains(h.Gender)) &&
                (filter.Breeds == null || filter.Breeds.Contains(h.Breed)) &&
                (!filter.MinAge.HasValue || h.Age >= filter.MinAge) &&
                (!filter.MaxAge.HasValue || h.Age <= filter.MaxAge);

            var result = await _genericService.FindAsync(predicate);
            return Ok(result);
        }
    }

}

