using System.ComponentModel.DataAnnotations;

namespace Sprint03.DTO
{
    public class PedidoItemInfo
    {
        [Required(ErrorMessage = "O Id do produto é obrigatório.")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }
    }
}
