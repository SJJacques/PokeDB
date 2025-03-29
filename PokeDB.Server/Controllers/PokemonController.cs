using Microsoft.AspNetCore.Mvc;
using PokeDB.Server.Models.DTOs;
using PokeDB.Server.Services.Interfaces;

namespace PokeDB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ICrudService<PokemonDto> _service;

        public PokemonController(ICrudService<PokemonDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonDto>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PokemonDto>> Create(PokemonDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return created != null
                ? CreatedAtAction(nameof(Get), new { id = created.Id }, created)
                : StatusCode(500, "Service could not retrieve created entity");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PokemonDto dto)
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
