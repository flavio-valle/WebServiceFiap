using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebServiceFiap.Model;
using WebServiceFiap.Repository.Context;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;

namespace WebServiceFiap.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string senha);
        Task<bool> RegisterAsync(string nome, string email, string senha, string role = "ROLE_USER");
    }

    public class AuthService : IAuthService
    {
        private readonly ContextBase _context;
        private readonly IConfiguration _configuration;

        public AuthService(ContextBase context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(string email, string senha)
        {
            var user = await _context.TB_USUARIO.FirstOrDefaultAsync(u => u.EMAIL == email);
            if (user == null) return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, user.SENHA);
            if (!senhaValida) return null;

            return GenerateToken(user);
        }

        public async Task<bool> RegisterAsync(string nome, string email, string senha, string role = "ROLE_USER")
        {
            // Verifica se o email já existe
            var existing = await _context.TB_USUARIO.FirstOrDefaultAsync(u => u.EMAIL == email);
            if (existing != null) return false; // Email já cadastrado

            // Hash da senha
            string hashedSenha = BCrypt.Net.BCrypt.HashPassword(senha);

            // Gerar um ID para o novo usuário
            int maxId = await _context.TB_USUARIO.MaxAsync(u => (int?)u.USUARIO_ID) ?? 0;
            int newId = maxId + 1;

            var user = new Usuario
            {
                USUARIO_ID = newId,
                NOME = nome,
                EMAIL = email,
                SENHA = hashedSenha,
                ROLE = role
            };

            _context.TB_USUARIO.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NOME),
                new Claim(ClaimTypes.NameIdentifier, user.USUARIO_ID.ToString()),
                new Claim(ClaimTypes.Email, user.EMAIL),
                new Claim(ClaimTypes.Role, user.ROLE ?? "ROLE_USER")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
