using University.Shared;

namespace University.Services.Abstractions
{
    public interface IStudentService
    {
        public Task<IEnumerable<StudentDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<StudentDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task CreateAsync(StudentToCreateDTO student, CancellationToken cancellationToken = default);
        public Task UpdateAsync(StudentToUpdateDTO student, CancellationToken cancellation = default);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<bool> CanBeCreatedAsync(CancellationToken cancellationToken = default);
    }
}
