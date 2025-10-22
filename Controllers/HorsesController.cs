using Microsoft.AspNetCore.Mvc;
using Hangfire;
using GameModel;
using System.Linq.Expressions;
using AutoMapper;

namespace YourNamespace.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class HorsesController : GenericController<Horse, HorseCreateDto, HorseShortDto>
{
        private readonly IHorseService _horseService;
        private readonly IImageService _imageService;
        private readonly IHorseBreedService _horseBreedService;
        private readonly IGenericService<Horse> _genericService;
        private readonly IMapper _mapper;

        public HorsesController(
            IHorseService horseService,
            IHorseBreedService horseBreedService,
            IGenericService<Horse> genericService,
            IImageService imageService,
            IMapper mapper)
            : base(genericService, mapper) 
        {
            _horseService = horseService;
            _horseBreedService = horseBreedService;
            _genericService = genericService;
            _imageService = imageService;
        }

        [HttpGet("random-name")]
        public IActionResult GetRandomHorseName(Gender gender)
        {
            var name = _horseService.GenerateRandomHorseName(gender);
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

        [HttpPost("create-horse")]
        public IActionResult CreateHorse(Guid id)
        {
            var horse = _horseService.CreateHorse(id);

            return Ok(new { horse }); 
        }

        [HttpPatch("update-horses-energy")]
        public IActionResult EnergyUpdateHorses()
        {
            BackgroundJob.Enqueue<IHorseService>(s => s.BatchHorsesAgeUpdate());
            return Ok(new { message = "Horses energy updated" }); //Update not sure if update ran?
        }

        [HttpPatch("update-horses-age")]
        public IActionResult AgeUpdateHorses()
        {
            BackgroundJob.Enqueue<IHorseService>(s => s.BatchHorsesAgeUpdate());
            return Ok(new { message = "Horses age updated" }); //Update not sure if update ran?
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] FileUploadRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _imageService.UploadImageAsync(request.File, request.FolderName);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchHorses([FromQuery] PaginationSearchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            var filter = request.Filter;

            Expression<Func<Horse, bool>> predicate = h =>
                (filter.Genders == null || filter.Genders.Contains(h.Gender)) &&
                (filter.Breeds == null || filter.Breeds.Contains(h.Breed)) &&
                (!filter.MinAge.HasValue || h.Age >= filter.MinAge) &&
                (!filter.MaxAge.HasValue || h.Age <= filter.MaxAge) &&
                (!filter.OwnerId.HasValue || h.OwnerId == filter.OwnerId) &&
                (!filter.SireId.HasValue || h.SireId == filter.SireId) &&
                (!filter.DamId.HasValue || h.DamId == filter.DamId);

            var result = await _genericService.GetPaginatedAsync<HorseShortDto>(request.Pagination, predicate);
            return Ok(result);
        }
    }

}

