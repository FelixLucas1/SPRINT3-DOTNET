using Microsoft.EntityFrameworkCore;
using Sprint03.Entidades;

namespace Sprint03.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<PedidoItem> PedidoItens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar o mapeamento para a entidade Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produto"); // Nome da tabela em maiúsculas
                entity.HasKey(p => p.Id); // Definir chave primária
                entity.Property(p => p.Id).HasColumnName("ID");
                entity.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(255).IsRequired();
                entity.Property(p => p.Preco).HasColumnName("PRECO").HasPrecision(10, 2);
                entity.Property(p => p.Descricao).HasColumnName("DESCRICAO").HasMaxLength(500);
            });

            // Configurar o mapeamento para a entidade Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("ID");
                entity.Property(u => u.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
                entity.Property(u => u.Email).HasColumnName("EMAIL").HasMaxLength(255).IsRequired();
            });

            // Configurar o mapeamento para a entidade Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedido");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnName("ID");
                entity.Property(p => p.UsuarioId).HasColumnName("USUARIOID");
                entity.Property(p => p.Data).HasColumnName("DATA").HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Configurar relacionamento com Usuario
                entity.HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey(p => p.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurar o mapeamento para a entidade PedidoItem
            modelBuilder.Entity<PedidoItem>(entity =>
            {
                entity.ToTable("PedidoItens");
                entity.HasKey(pi => pi.Id);
                entity.Property(pi => pi.Id).HasColumnName("ID");
                entity.Property(pi => pi.PedidoId).HasColumnName("PEDIDOID");
                entity.Property(pi => pi.ProdutoId).HasColumnName("PRODUTOID");
                entity.Property(pi => pi.Quantidade).HasColumnName("QUANTIDADE");

                // Configurar relacionamento com Pedido
                entity.HasOne<Pedido>()
                    .WithMany(p => p.Itens)
                    .HasForeignKey(pi => pi.PedidoId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configurar relacionamento com Produto
                entity.HasOne<Produto>()
                    .WithMany()
                    .HasForeignKey(pi => pi.ProdutoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}