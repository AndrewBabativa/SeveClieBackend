using Microsoft.EntityFrameworkCore;
using SeveClie.Domain.Entities;

namespace SeveClie.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ClieEntity> Clie { get; set; }
    }
}
