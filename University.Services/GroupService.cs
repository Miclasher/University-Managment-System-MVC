﻿using Mapster;
using University.Domain.Models;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    public sealed class GroupService : IGroupService
    {
        private readonly IRepositoryManager _repositoryManager;

        public GroupService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<GroupDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var groups = await _repositoryManager.Group.GetAllAsync(cancellationToken);

            return groups.Adapt<IEnumerable<GroupDTO>>();
        }

        public async Task<GroupDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var group = await _repositoryManager.Group.GetByIdAsync(id, cancellationToken);

            if (group is null)
            {
                throw new KeyNotFoundException($"Group with id {id} not found. It is possible that someone else deleted this group.");
            }

            return group.Adapt<GroupDTO>();
        }

        public async Task CreateAsync(GroupToCreateDTO group, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(group, nameof(group));

            var newGroup = group.Adapt<Group>();

            await _repositoryManager.Group.AddAsync(newGroup, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var group = await _repositoryManager.Group.GetByIdAsync(id, cancellationToken);

            if (group is null)
            {
                throw new KeyNotFoundException($"Group with id {id} not found. It is possible that someone else deleted this group.");
            }

            if (group.Students.Any())
            {
                throw new InvalidOperationException("Group cannot be deleted because it has students.");
            }

            _repositoryManager.Group.Remove(group, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(GroupToUpdateDTO group, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(group, nameof(group));

            var groupToUpdate = await _repositoryManager.Group.GetByIdAsync(group.Id, cancellation);

            if (groupToUpdate is null)
            {
                throw new KeyNotFoundException($"Group with id {group.Id} not found. It is possible that someone else deleted this group.");
            }

            groupToUpdate.Name = group.Name;
            groupToUpdate.CourseId = group.CourseId;
            groupToUpdate.TeacherId = group.TeacherId;

            _repositoryManager.Group.Update(groupToUpdate, cancellation);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellation);
        }

        public async Task<bool> CanBeCreatedAsync(CancellationToken cancellationToken = default)
        {
            return (await _repositoryManager.Course.GetAllAsync(cancellationToken)).Any()
                && (await _repositoryManager.Teacher.GetAllAsync(cancellationToken)).Any();
        }

        public async Task ClearGroupAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var group = await _repositoryManager.Group.GetByIdAsync(id, cancellationToken);

            if (group is null)
            {
                throw new KeyNotFoundException($"Group with id {id} not found. It is possible that someone else deleted this group.");
            }

            await _repositoryManager.Group.DeleteStudentsFromGroup(id, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
