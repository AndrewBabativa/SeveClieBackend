using Microsoft.EntityFrameworkCore;
using SeveClie.Domain.Entities;

namespace SeveClie.Infrastructure.Repositories
{
    public class ClieRepository : IClieRepository
    {
        private readonly ApplicationDbContext _context;

        public ClieRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(ClieEntity entity)
        {
            try {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await _context.Clie.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }

        }

        public async Task<ClieEntity?> GetByIdAsync(int id)
        {
            return await _context.Clie.FindAsync(id);
        }

        public async Task<IEnumerable<ClieEntity>> GetAllAsync()
        {
            return await _context.Clie.ToListAsync();
        }

        public async Task UpdateAsync(ClieEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Clie.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Clie.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
