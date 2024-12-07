using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.DTOs;
using WebServiceFiap.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebServiceFiap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await _authService.LoginAsync(loginDto.Email, loginDto.Senha);
            if (token == null)
                return Unauthorized("Credenciais inválidas.");

            return Ok(new { token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool success = await _authService.RegisterAsync(registerDto.Nome, registerDto.Email, registerDto.Senha, registerDto.Role);
            if (!success)
                return Conflict("Email já cadastrado.");

            return Created("", new { message = "Usuário criado com sucesso." });
        }
    }
}
