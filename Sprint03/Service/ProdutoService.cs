using Microsoft.EntityFrameworkCore;
using Sprint03.Context;
using Sprint03.DTOs;
using Sprint03.Entidades;

namespace Sprint03.Service
{
    public class ProdutoService
    {
        private readonly Sprint03.Repository.ProdutoRepository _repo;
        private readonly AppDbContext _context; // Add the DbContext field

        // Fix the constructor to receive DbContext
        public ProdutoService(
            Sprint03.Repository.ProdutoRepository repo,
            AppDbContext context) // Add this parameter
        {
            _repo = repo;
            _context = context; // Initialize the context
        }

        public async Task<(List<Pedido> Pedidos, int TotalCount, int TotalPages)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(pi => pi.Produto)
                .Include(p => p.UsuarioId)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var pedidos = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return (pedidos, totalCount, totalPages);
        }

        public Task<Produto?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);


        public async Task<Produto> CreateAsync(ProdutoDto dto)
        {
            var p = new Produto { Nome = dto.Nome, Preco = dto.Preco, Descricao = dto.Descricao };
            await _repo.AddAsync(p);
            return p;
        }

        public async Task<bool> UpdateAsync(int id, ProdutoDto dto)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;
            p.Nome = dto.Nome;
            p.Preco = dto.Preco;
            p.Descricao = dto.Descricao;
            await _repo.UpdateAsync(p);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;
            await _repo.DeleteAsync(p);
            return true;
        }

        public Task<List<Produto>> GetByIdsAsync(IEnumerable<int> ids) => _repo.GetByIdsAsync(ids);
    }
}
