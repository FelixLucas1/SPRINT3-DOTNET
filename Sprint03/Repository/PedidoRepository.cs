using Microsoft.EntityFrameworkCore;
using Sprint03.Context;
using Sprint03.Entidades;

namespace Sprint03.Repository
{
    public class PedidoRepository
    {
        private readonly AppDbContext _db;
        public PedidoRepository(AppDbContext db) => _db = db;

        public Task<List<Pedido>> GetAllWithItemsAsync() =>
            _db.Pedidos
               .Include(p => p.Itens)
                 .ThenInclude(i => i.Produto)
               .OrderByDescending(p => p.Data)
               .AsNoTracking()
               .ToListAsync();

        public Task<Pedido?> GetWithItemsAsync(int id) =>
            _db.Pedidos
               .Include(p => p.Itens)
                 .ThenInclude(i => i.Produto)
               .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Pedido pedido)
        {
            _db.Pedidos.Add(pedido);
            await _db.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int pedidoId)
        {
            var pedido = await _db.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == pedidoId);
            if (pedido == null) return;
            _db.Pedidos.Remove(pedido);
            await _db.SaveChangesAsync();
        }
    }
}
