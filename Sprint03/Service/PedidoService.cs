using Microsoft.EntityFrameworkCore;
using Sprint03.Context;
using Sprint03.DTO;
using Sprint03.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprint03.Service
{
    public class PedidoService
    {
        private readonly Sprint03.Repository.PedidoRepository _pedidoRepo;
        private readonly Sprint03.Repository.UsuarioRepository _usuarioRepo;
        private readonly Sprint03.Repository.ProdutoRepository _produtoRepo;
        private readonly AppDbContext _context; // Mude para AppDbContext

        public PedidoService(
            Sprint03.Repository.PedidoRepository pedidoRepo,
            Sprint03.Repository.UsuarioRepository usuarioRepo,
            Sprint03.Repository.ProdutoRepository produtoRepo,
            AppDbContext context) // Mude para AppDbContext
        {
            _pedidoRepo = pedidoRepo;
            _context = context;
            _usuarioRepo = usuarioRepo;
            _produtoRepo = produtoRepo;
        }

        public Task<List<Pedido>> GetAllAsync() => _pedidoRepo.GetAllWithItemsAsync();

        public Task<Pedido?> GetByIdAsync(int id) => _pedidoRepo.GetWithItemsAsync(id);

        public async Task<(bool Success, string? Error, Pedido? Pedido)> CreateAsync(PedidoDto dto)
        {
            var user = await _usuarioRepo.GetByIdAsync(dto.UsuarioId);
            if (user == null) return (false, "Usuario não encontrado.", null);

            // valida produtos
            var ids = dto.Itens.Select(i => i.ProdutoId).Distinct().ToList();
            var produtos = await _produtoRepo.GetByIdsAsync(ids);
            var missing = ids.Except(produtos.Select(p => p.Id)).ToList();
            if (missing.Any()) return (false, $"Produtos inexistentes: {string.Join(',', missing)}", null);

            var pedido = new Pedido
            {
                UsuarioId = dto.UsuarioId,
                Data = dto.Data ?? System.DateTime.UtcNow
            };
            foreach (var it in dto.Itens)
                pedido.Itens.Add(new PedidoItem { ProdutoId = it.ProdutoId, Quantidade = it.Quantidade });

            await _pedidoRepo.AddAsync(pedido);

            var created = await _pedidoRepo.GetWithItemsAsync(pedido.Id);
            return (true, null, created ?? pedido);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _pedidoRepo.GetWithItemsAsync(id);
            if (p == null) return false;
            await _pedidoRepo.DeleteAsync(id);
            return true;
        }

        public async Task<(List<Pedido> Pedidos, int TotalCount, int TotalPages)> GetPaginatedAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(pi => pi.Produto)
                .Include(p => p.UsuarioId) // Se você quiser incluir o usuário também
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var pedidos = await query
                .OrderBy(p => p.Id) // É bom ter uma ordenação definida para paginação
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return (pedidos, totalCount, totalPages);
        }
    }
}


