using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    [Route("api/competitions")]
    public class CompetitionsController : GenericController<Competition>
    {
        public CompetitionsController(IGenericService<Competition> service) : base(service) { }

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/horsebreeds")]
    public class HorseBreedsController : GenericController<HorseBreed>
    {
        public HorseBreedsController(IGenericService<HorseBreed> service) : base(service) {}

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/levels")]
    public class LevelsController : GenericController<Level>
    {
        public LevelsController(IGenericService<Level> service) : base(service) { }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id)
        {
            return BadRequest("Delete operation is not allowed for this entity.");
        }
    }
}
