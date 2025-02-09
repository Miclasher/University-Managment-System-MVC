using Microsoft.EntityFrameworkCore;
using University.Domain.Models;
using University.Domain.Repositories;

namespace University.Infrastructure.Repositories
{
    internal sealed class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(UniversityDbContext context) : base(context)
        {
        }

        public async override Task<Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var teacher = await _dbSet
                .Include(t => t.Groups)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            return teacher!;
        }
    }
}
