using Microsoft.EntityFrameworkCore;
using University.Domain.Models;
using University.Domain.Repositories;

namespace University.Infrastructure.Repositories
{
    internal sealed class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(UniversityDbContext context) : base(context)
        {
        }

        public async override Task<Course> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var course = await _dbSet
                .Include(c => c.Groups)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            return course!;
        }
    }
}
