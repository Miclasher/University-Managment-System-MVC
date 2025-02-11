using Mapster;
using University.Domain.Models;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    public sealed class TeacherService : ITeacherService
    {
        private readonly IRepositoryManager _repositoryManager;

        public TeacherService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<TeacherDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var teachers = await _repositoryManager.Teacher.GetAllAsync(cancellationToken);

            return teachers.Adapt<IEnumerable<TeacherDTO>>();
        }

        public async Task<TeacherDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var teacher = await _repositoryManager.Teacher.GetByIdAsync(id, cancellationToken);

            if (teacher is null)
            {
                throw new KeyNotFoundException($"Teacher with id {id} not found. It is possible that someone else deleted this teacher.");
            }

            return teacher.Adapt<TeacherDTO>();
        }

        public async Task CreateAsync(TeacherToCreateDTO teacher, CancellationToken cancellationToken = default)
        {
            var newTeacher = new Teacher()
            {
                Id = Guid.NewGuid(),
                FirstName = teacher.FirstName,
                LastName = teacher.LastName
            };

            await _repositoryManager.Teacher.AddAsync(newTeacher, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var teacher = await _repositoryManager.Teacher.GetByIdAsync(id, cancellationToken);

            if (teacher is null)
            {
                throw new KeyNotFoundException($"Teacher with id {id} not found. It is possible that someone else deleted this teacher.");
            }

            if (teacher.Groups.Any())
            {
                throw new InvalidOperationException("Teacher with cannot be deleted because it has groups.");
            }

            _repositoryManager.Teacher.Remove(teacher, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TeacherToUpdateDTO teacher, CancellationToken cancellation = default)
        {
            var teacherToUpdate = await _repositoryManager.Teacher.GetByIdAsync(teacher.Id, cancellation);

            if (teacherToUpdate is null)
            {
                throw new KeyNotFoundException($"Teacher with id {teacher.Id} not found. It is possible that someone else deleted this teacher.");
            }

            teacherToUpdate.FirstName = teacher.FirstName;
            teacherToUpdate.LastName = teacher.LastName;

            _repositoryManager.Teacher.Update(teacherToUpdate, cancellation);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellation);
        }
    }
}
