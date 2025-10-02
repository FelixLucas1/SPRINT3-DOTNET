using Microsoft.EntityFrameworkCore;
using Sprint03.Context;
using Sprint03.DTO;
using Sprint03.DTOs;
using Sprint03.Entidades;

namespace Sprint03.Service
{
    public class UsuarioService
    {
        private readonly Sprint03.Repository.UsuarioRepository _repo;
        private readonly AppDbContext _context; // Adicione este campo


        public UsuarioService(Sprint03.Repository.UsuarioRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context; 
        }


        public async Task<(List<Pedido> Pedidos, int TotalCount, int TotalPages)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Pedidos // Agora _context existe!
                .Include(p => p.Itens)
                    .ThenInclude(pi => pi.Produto)
                .Include(p => p.UsuarioId)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            // Aplicar paginação
            var pedidos = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calcular total de páginas
            var totalPages = (int)System.Math.Ceiling(totalCount / (double)pageSize);

            return (pedidos, totalCount, totalPages);
        }

        public Task<Usuario?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<Usuario> CreateAsync(UsuarioDto dto)
        {
            var u = new Usuario { Nome = dto.Nome, Email = dto.Email };
            await _repo.AddAsync(u);
            return u;
        }

        public async Task<bool> UpdateAsync(int id, UsuarioDto dto)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null) return false;
            u.Nome = dto.Nome;
            u.Email = dto.Email;
            await _repo.UpdateAsync(u);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null) return false;
            await _repo.DeleteAsync(u);
            return true;
        }
    }
}
