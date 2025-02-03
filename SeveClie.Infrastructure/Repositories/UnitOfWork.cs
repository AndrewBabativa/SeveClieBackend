namespace SeveClie.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IClieRepository ClieRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ClieRepository = new ClieRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
