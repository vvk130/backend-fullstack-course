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
        private readonly AppDbContext _context;

        public HorsesController(
            IHorseService horseService,
            IHorseBreedService horseBreedService,
            AppDbContext context,
            IGenericService<Horse> genericService,
            IImageService imageService,
            IMapper mapper)
            : base(genericService, mapper) 
        {
            _horseService = horseService;
            _horseBreedService = horseBreedService;
            _genericService = genericService;
            _imageService = imageService;
            _context = context;
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
        public IActionResult CreateHorse(Guid id, Breed? breed = null)
        {
            var horse = _horseService.CreateHorse(id, breed);

            return Ok(new { horse }); 
        }

        [HttpPost("create-alpaca")]
        public IActionResult CreateAlpaca(Guid id, AlpacaBreed? breed = null)
        {
            var alpaca = _horseService.CreateAlpaca(id, breed);

            return Ok(new { alpaca }); 
        }

        [HttpPatch("update-horses-energy")]
        public IActionResult EnergyUpdateHorses()
        {
            BackgroundJob.Enqueue<IHorseService>(s => s.BatchHorsesAgeUpdate());
            return Ok(new { message = "Horses energy updated" }); 
        }

        [HttpPatch("update-horses-age")]
        public IActionResult AgeUpdateHorses()
        {
            BackgroundJob.Enqueue<IHorseService>(s => s.BatchHorsesAgeUpdate());
            return Ok(new { message = "Horses age updated" }); 
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

        [HttpGet("search-ads")]
        public async Task<IActionResult> SearchHorsesAds([FromQuery] PaginationRequest request)
        {
            var result = await _genericService.GetPaginatedAdsWithItemsAsync<Horse, HorseShortDto>(
                ItemType.Horse,
                request.PageNumber,
                request.PageSize
            );

            return Ok(result);
        }

        [HttpDelete("horses/clear")]
        public async Task<IActionResult> DeleteAllHorses()
        {
            try
            {
                // Retrieve all horses
                var allHorses = await _context.Horses.ToListAsync();

                if (!allHorses.Any())
                    return NotFound("No horses found to delete.");

                // Remove all
                _context.Horses.RemoveRange(allHorses);

                // Commit changes
                await _context.SaveChangesAsync();

                return Ok($"{allHorses.Count} horses deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging
                return StatusCode(500, $"Error deleting horses: {ex.Message}");
            }
        }

}
}

