using University.Domain.Repositories;

namespace University.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityDbContext _context;

        public UnitOfWork(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
