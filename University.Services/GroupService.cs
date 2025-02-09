using Mapster;
using University.Domain.Models;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    internal sealed class GroupService : IGroupService
    {
        private readonly IRepositoryManager _repositoryManager;

        public GroupService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<GroupDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var Groups = await _repositoryManager.Group.GetAllAsync(cancellationToken);

            return Groups.Adapt<IEnumerable<GroupDTO>>();
        }

        public async Task<GroupDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Group = await _repositoryManager.Group.GetByIdAsync(id, cancellationToken);

            if (Group is null)
            {
                throw new KeyNotFoundException($"Group with id {id} not found");
            }

            return Group.Adapt<GroupDTO>();
        }

        public async Task CreateAsync(GroupToCreateDTO group, CancellationToken cancellationToken = default)
        {
            var newGroup = new Group()
            {
                Id = Guid.NewGuid(),
                Name = group.Name,
                CourseId = group.CourseId,
                TeacherId = group.TeacherId
            };

            await _repositoryManager.Group.AddAsync(newGroup, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Group = await _repositoryManager.Group.GetByIdAsync(id, cancellationToken);

            if (Group is null)
            {
                throw new KeyNotFoundException($"Group with id {id} not found");
            }

            _repositoryManager.Group.Remove(Group, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(GroupToUpdateDTO group, CancellationToken cancellation = default)
        {
            var GroupToUpdate = await _repositoryManager.Group.GetByIdAsync(group.Id, cancellation);

            if (GroupToUpdate is null)
            {
                throw new KeyNotFoundException($"Group with id {group.Id} not found");
            }

            GroupToUpdate.Name = group.Name;
            GroupToUpdate.CourseId = group.CourseId;
            GroupToUpdate.TeacherId = group.TeacherId;

            _repositoryManager.Group.Update(GroupToUpdate, cancellation);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellation);
        }
    }
}
