namespace Sprint03.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public List<PedidoItem> Itens { get; set; } = new();
    }
}
