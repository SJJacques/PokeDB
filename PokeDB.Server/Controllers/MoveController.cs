using Microsoft.AspNetCore.Mvc;
using PokeDB.Server.Models.DTOs;
using PokeDB.Server.Services.Interfaces;

namespace PokeDB.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoveController : ControllerBase
    {
        private readonly ICrudService<MoveDto> _service;

        public MoveController(ICrudService<MoveDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoveDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MoveDto>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MoveDto>> Create(MoveDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return created != null
                ? CreatedAtAction(nameof(Get), new { id = created.Id }, created)
                : StatusCode(500, "Service could not retrieve created entity");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MoveDto dto)
        {
            return await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
