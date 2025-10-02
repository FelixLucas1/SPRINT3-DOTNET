namespace Sprint03.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
    }
}
