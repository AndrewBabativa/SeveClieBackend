namespace SeveClie.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClieRepository ClieRepository { get; }
        Task<int> CompleteAsync();
    }
}
