using System.ComponentModel.DataAnnotations;

namespace WebServiceFiap.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome pode ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        [StringLength(100, ErrorMessage = "O email pode ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(255, ErrorMessage = "A senha pode ter no máximo 255 caracteres.")]
        public string Senha { get; set; }

        public string Role { get; set; } = "ROLE_USER";
    }
}
