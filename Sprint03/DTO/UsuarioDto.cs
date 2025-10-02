using System.ComponentModel.DataAnnotations;

namespace Sprint03.DTOs
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [StringLength(200, ErrorMessage = "Email deve ter no máximo 200 caracteres.")]
        public string Email { get; set; } = null!;
    }
}