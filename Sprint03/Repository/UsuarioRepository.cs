using Microsoft.EntityFrameworkCore;
using Sprint03.Context;
using Sprint03.Entidades;

namespace Sprint03.Repository
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _db;
        public UsuarioRepository(AppDbContext db) => _db = db;

        public Task<List<Usuario>> GetAllAsync() => _db.Usuarios.AsNoTracking().ToListAsync();

        public Task<Usuario?> GetByIdAsync(int id) => _db.Usuarios.FindAsync(id).AsTask();

        public async Task AddAsync(Usuario u)
        {
            await _db.Usuarios.AddAsync(u);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario u)
        {
            _db.Usuarios.Update(u);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario u)
        {
            _db.Usuarios.Remove(u);
            await _db.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id) => _db.Usuarios.AnyAsync(x => x.Id == id);
    }    

}
