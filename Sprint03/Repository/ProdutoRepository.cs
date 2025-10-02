using Microsoft.EntityFrameworkCore;
using Sprint03.Context;
using Sprint03.Controller;
using Sprint03.Entidades;

namespace Sprint03.Repository
{
    public class ProdutoRepository
    {
        private readonly AppDbContext _db;
        public ProdutoRepository(AppDbContext db) => _db = db;

        public Task<List<Produto>> GetAllAsync() => _db.Produtos.AsNoTracking().OrderBy(p => p.Nome).ToListAsync();

        public Task<Produto?> GetByIdAsync(int id) => _db.Produtos.FindAsync(id).AsTask();

        public async Task AddAsync(Produto p)
        {
            await _db.Produtos.AddAsync(p);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto p)
        {
            _db.Produtos.Update(p);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produto u)
        {
            _db.Produtos.Remove(u);
            await _db.SaveChangesAsync();
        }

        public Task<List<Produto>> GetByIdsAsync(IEnumerable<int> ids) =>
            _db.Produtos.Where(x => ids.Contains(x.Id)).ToListAsync();

    }
}
