using SeveClie.Domain.Entities;

namespace SeveClie.Infrastructure.Repositories
{
    public interface IClieRepository
    {
        Task AddAsync(ClieEntity entity);
        Task<ClieEntity?> GetByIdAsync(int id);
        Task<IEnumerable<ClieEntity>> GetAllAsync();
        Task UpdateAsync(ClieEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
