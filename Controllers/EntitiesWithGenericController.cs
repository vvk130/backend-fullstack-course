using Microsoft.AspNetCore.Mvc;
using GameModel;
using System.Security.Claims;

namespace YourProject.Controllers
{
    [Route("api/competitions")]
    // [Authorize(Roles = "Admin")]
    public class CompetitionsController : GenericController<Competition, CompetitionDto>
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionsController(ICompetitionService competitionService, IGenericService<Competition> service) : base(service) 
        { 
            _competitionService = competitionService;
        }

            [HttpPost("compete-horses")]
            public async Task<IActionResult> CompeteHorses([FromBody] CompetitionRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _competitionService.GetCompetitionResult(request.CompetitionId, request.HorseIds);
                return Ok(result);
            }

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/horsebreeds")]
    public class HorseBreedsController : GenericController<HorseBreed, BreedShortDto>
    {
        public HorseBreedsController(IGenericService<HorseBreed> service) : base(service) {}

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/levels")]
    public class LevelsController : GenericController<Level, LevelShortDto>
    {
        public LevelsController(IGenericService<Level> service) : base(service) { }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id)
        {
            return BadRequest("Delete operation is not allowed for this entity.");
        }
    }

    [Route("api/wallet")]
    public class WalletController : GenericController<Wallet, WalletDto>
    {
        public WalletController(IGenericService<Wallet> service) : base(service) {}

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/salesad")]
    [Authorize]
    public class SalesAdController : GenericController<SalesAd, SalesAdDto>
    {
            private readonly IGenericService<SalesAd> _adService;
            private readonly IGenericService<Horse> _horseService;

            public SalesAdController(
                IGenericService<SalesAd> adService,
                IGenericService<Horse> horseService) : base(adService)
            {
                _adService = adService;
                _horseService = horseService;
            }

            [HttpPost("create-sales-ad")]
            public async Task<IActionResult> Create([FromBody] SalesAdRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var ownerId = Guid.Parse(userId);

                var horse = await _horseService.FindAsync(h =>
                    h.Id == request.HorseId && h.OwnerId == ownerId);

                if (horse is null)
                    return Forbid("You don't own this horse.");

                var ad = new SalesAd
                {
                    HorseId = request.HorseId,
                    OwnerId = ownerId,
                    Price = request.Price,
                    StartTime = DateTime.UtcNow,
                    EndTime = request.EndTime,
                    AdType = request.AdType
                };

                await _adService.AddAsync(ad);
                return Ok();
            }


            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }
}
