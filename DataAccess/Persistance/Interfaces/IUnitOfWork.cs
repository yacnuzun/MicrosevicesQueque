namespace Shared.Persistance.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        void Rollback();
    }
}
