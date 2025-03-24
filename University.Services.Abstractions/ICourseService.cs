using University.Shared;

namespace University.Services.Abstractions
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<CourseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<CourseDTO> GetCourseWithGroupDetailsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task CreateAsync(CourseToCreateDTO course, CancellationToken cancellationToken = default);
        public Task UpdateAsync(CourseToUpdateDTO course, CancellationToken cancellation = default);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
