using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class GenericController<TEntity> : ControllerBase where TEntity : class
{
    private readonly IGenericService<TEntity> _genericService;

    public GenericController(IGenericService<TEntity> genericService)
    {
        _genericService = genericService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
    {
        var entities = await _genericService.GetAllAsync();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TEntity>> GetById(Guid id)
    {
        var entity = await _genericService.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TEntity entity)
    {
        await _genericService.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        var entity = await _genericService.GetByIdAsync(id);
        if (entity == null) return NotFound();

        await _genericService.RemoveAsync(entity);
        return NoContent();
    }
}
