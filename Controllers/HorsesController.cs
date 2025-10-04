// using Microsoft.AspNetCore.Mvc;

// namespace YourNamespace.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class YourEntityController : ControllerBase
//     {
//         [HttpGet]
//         public IActionResult GetAll()
//         {
//             return Ok(new { message = "Get all entities" });
//         }

//         [HttpGet("{id}")]
//         public IActionResult GetById(int id)
//         {
//             return Ok(new { message = $"Get entity with id {id}" });
//         }

//         [HttpPost]
//         public IActionResult Create([FromBody] YourEntityDto dto)
//         {
//             return CreatedAtAction(nameof(GetById), new { id = 1 }, dto);
//         }

//         [HttpPut("{id}")]
//         public IActionResult Update(int id, [FromBody] YourEntityDto dto)
//         {
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public IActionResult Delete(int id)
//         {
//             return NoContent();
//         }
//     }

// }
