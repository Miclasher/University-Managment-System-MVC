using University.Domain.Models;

namespace University.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetCourseWithGroupDetailsByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
