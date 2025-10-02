using Sprint03.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Sprint03.DTO
{
    public class PedidoDto
    {
        [Required(ErrorMessage = "O Id do usuário é obrigatório.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        [MinLength(1, ErrorMessage = "Deve haver ao menos um item no pedido.")]
        public List<PedidoItemInfo> Itens { get; set; } = new();

        public DateTime? Data { get; set; } = DateTime.UtcNow;
    }
}
