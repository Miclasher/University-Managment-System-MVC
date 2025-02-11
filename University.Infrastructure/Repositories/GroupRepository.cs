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

        public async override Task<IEnumerable<Group>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var groups = await _dbSet
                 .Include(g => g.Course)
                .Include(g => g.Teacher)
                .Include(g => g.Students)
                .ToListAsync(cancellationToken);

            return groups;
        }

        public async Task DeleteStudentsFromGroup(Guid id, CancellationToken cancellationToken = default)
        {
            var studentsDbSet = _context.Set<Student>();

            var students = await studentsDbSet
                .Where(s => s.GroupId == id)
                .ToListAsync(cancellationToken);

            studentsDbSet.RemoveRange(students);
        }
    }
}
