using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.Model;
using WebServiceFiap.Services;

namespace WebServiceFiap.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoAguaController : ControllerBase
    {
        private readonly MonitoramentoAguaService _service;

        public MonitoramentoAguaController(MonitoramentoAguaService service)
        {
            _service = service;
        }

        // GET /api/monitoramentoagua?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (items, total) = await _service.GetAllPagedAsync(pageNumber, pageSize);
            return Ok(new { data = items, totalCount = total });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound("Registro não encontrado.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MonitoramentoAgua body)
        {
            await _service.AddAsync(body);
            return CreatedAtAction(nameof(GetById), new { id = body.ID_MONITORAMENTO_AGUA }, body);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MonitoramentoAgua body)
        {
            try
            {
                await _service.UpdateAsync(id, body);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Registro não encontrado.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
