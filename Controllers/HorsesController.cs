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
        private readonly IGenericService<Horse> _genericService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public HorsesController(
            IHorseService horseService,
            AppDbContext context,
            IGenericService<Horse> genericService,
            IImageService imageService,
            IMapper mapper)
            : base(genericService, mapper) 
        {
            _horseService = horseService;
            _genericService = genericService;
            _imageService = imageService;
            _context = context;
        }

        [HttpPost("create-horse")]
        public IActionResult CreateHorse([FromBody] HorseCreateForUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var horse = _horseService.CreateHorse(dto.Id, dto.Breed);

            return Ok(new { horse }); 
        }

        [HttpPost("create-alpaca")]
        public IActionResult CreateAlpaca([FromBody] AlpacaCreateForUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var alpaca = _horseService.CreateAlpaca(dto.Id, dto.AlpacaBreed);

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

}
}

