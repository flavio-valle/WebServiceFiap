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
        [ProducesResponseType(typeof(object), 200)]  // Sucesso
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(500)]  // InternalServerError
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Número da página e tamanho devem ser maiores que zero.");
            }

            try
            {
                var (items, total) = await _service.GetAllPagedAsync(pageNumber, pageSize);
                return Ok(new { data = items, totalCount = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao recuperar dados: {ex.Message}");
            }
        }

        // GET /api/monitoramentoagua/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MonitoramentoAgua), 200)]  // Sucesso
        [ProducesResponseType(404)]  // NotFound
        [ProducesResponseType(500)]  // InternalServerError
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null) return NotFound("Registro não encontrado.");
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao recuperar o registro: {ex.Message}");
            }
        }

        // POST /api/monitoramentoagua
        [HttpPost]
        [ProducesResponseType(typeof(MonitoramentoAgua), 201)]  // Created
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(500)]  // InternalServerError
        public async Task<IActionResult> Post([FromBody] MonitoramentoAgua body)
        {
            if (body == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                await _service.AddAsync(body);
                return CreatedAtAction(nameof(GetById), new { id = body.ID_MONITORAMENTO_AGUA }, body);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar o registro: {ex.Message}");
            }
        }

        // PUT /api/monitoramentoagua/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(204)]  // NoContent
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(404)]  // NotFound
        [ProducesResponseType(500)]  // InternalServerError
        public async Task<IActionResult> Put(int id, [FromBody] MonitoramentoAgua body)
        {
            if (body == null || id <= 0)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                await _service.UpdateAsync(id, body);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Registro não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o registro: {ex.Message}");
            }
        }

        // DELETE /api/monitoramentoagua/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]  // NoContent
        [ProducesResponseType(404)]  // NotFound
        [ProducesResponseType(500)]  // InternalServerError
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound("Registro não encontrado.");
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar o registro: {ex.Message}");
            }
        }
    }
}
