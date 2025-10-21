using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class GenericController<TEntity, TDto> : ControllerBase where TEntity : class where TDto : class 
{
    private readonly IGenericService<TEntity> _genericService;
    private readonly IMapper _mapper;

    public GenericController(IGenericService<TEntity> genericService, IMapper mapper)
    {
        _genericService = genericService;
        _mapper = mapper;
    }

    [HttpGet("paginated")]
    public virtual async Task<IActionResult> GetPaginated([FromQuery] PaginationRequest pagination)
    {
        var result = await _genericService.GetPaginatedAsync<TDto>(pagination);
        return Ok(result);
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _genericService.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id == null) 
            return NotFound();
        
        var existingEntity = await _genericService.GetByIdAsync(id);
        if (existingEntity == null) 
            return NotFound();

        _mapper.Map(dto, existingEntity);
        
        await _genericService.UpdateAsync(existingEntity);

        return Ok(id);
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
