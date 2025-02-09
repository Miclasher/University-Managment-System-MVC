using Microsoft.EntityFrameworkCore;
using University.Domain.Models;
using University.Domain.Repositories;

namespace University.Infrastructure.Repositories
{
    internal sealed class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(UniversityDbContext context) : base(context)
        {
        }

        public async override Task<Group> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var group = await _dbSet
                .Include(g => g.Course)
                .Include(g => g.Teacher)
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

            return group!;
        }
    }
}
