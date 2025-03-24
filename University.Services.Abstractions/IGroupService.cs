using University.Shared;

namespace University.Services.Abstractions
{
    public interface IGroupService
    {
        public Task<IEnumerable<GroupDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<GroupDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task CreateAsync(GroupToCreateDTO group, CancellationToken cancellationToken = default);
        public Task UpdateAsync(GroupToUpdateDTO group, CancellationToken cancellation = default);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<bool> CanBeCreatedAsync(CancellationToken cancellationToken = default);
        public Task ClearGroupAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
