using System.ComponentModel.DataAnnotations;

namespace Sprint03.DTOs
{
    public class ProdutoDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "Nome deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [StringLength(500, ErrorMessage = "Descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }
    }
}
