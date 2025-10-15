using Microsoft.AspNetCore.Mvc;
using GameModel;

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
}
