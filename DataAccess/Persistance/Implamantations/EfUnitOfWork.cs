using Microsoft.EntityFrameworkCore;
using Shared.Persistance.Interfaces;

namespace Shared.Persistance.Implamantations
{
    public class EfUnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
    {
        private readonly TContext _context;
        public EfUnitOfWork(TContext context) => _context = context;

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);

        public void Rollback()
        {
            // Rollback by resetting tracked entities
            var entries = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();
            foreach (var entry in entries)
                entry.State = EntityState.Unchanged;
        }

        public void Dispose() => _context.Dispose();
    }
}
