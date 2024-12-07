using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.Model;
using WebServiceFiap.Services;

namespace WebServiceFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoArController : ControllerBase
    {
        private readonly MonitoramentoArService _service;

        public MonitoramentoArController(MonitoramentoArService service)
        {
            _service = service;
        }

        // GET /api/monitoramentoar?pageNumber=1&pageSize=10
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
        public async Task<IActionResult> Post([FromBody] MonitoramentoAr body)
        {
            await _service.AddAsync(body);
            return CreatedAtAction(nameof(GetById), new { id = body.ID_MONITORAMENTO_AR }, body);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MonitoramentoAr body)
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
