using Microsoft.EntityFrameworkCore;
using University.Domain.Models;
using University.Domain.Repositories;

namespace University.Infrastructure.Repositories
{
    internal sealed class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(UniversityDbContext context) : base(context)
        {
        }

        public async override Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var student = await _dbSet
                .Include(s => s.Group)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            return student!;
        }
    }
}
