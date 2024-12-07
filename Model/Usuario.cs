
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Model
{
    [Table("TB_USUARIO", Schema = "RM554222")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int USUARIO_ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string NOME { get; set; }

        [Required]
        [MaxLength(100)]
        public string EMAIL { get; set; }

        [Required]
        [MaxLength(255)]
        public string SENHA { get; set; }

        [MaxLength(50)]
        public string ROLE { get; set; } = "ROLE_USER";
    }
}
