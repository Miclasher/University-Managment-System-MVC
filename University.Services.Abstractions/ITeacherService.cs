using University.Shared;

namespace University.Services.Abstractions
{
    public interface ITeacherService
    {
        public Task<IEnumerable<TeacherDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<TeacherDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task CreateAsync(TeacherToCreateDTO teacher, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TeacherToUpdateDTO teacher, CancellationToken cancellation = default);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
