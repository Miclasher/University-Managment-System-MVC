using Mapster;
using University.Domain.Models;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    public sealed class StudentService : IStudentService
    {
        private readonly IRepositoryManager _repositoryManager;

        public StudentService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var Students = await _repositoryManager.Student.GetAllAsync(cancellationToken);

            return Students.Adapt<IEnumerable<StudentDTO>>();
        }

        public async Task<StudentDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Student = await _repositoryManager.Student.GetByIdAsync(id, cancellationToken);

            if (Student is null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found. It is possible that someone else deleted this student.");
            }

            return Student.Adapt<StudentDTO>();
        }

        public async Task CreateAsync(StudentToCreateDTO student, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(student, nameof(student));

            var newStudent = new Student()
            {
                Id = Guid.NewGuid(),
                FirstName = student.FirstName,
                LastName = student.LastName,
                GroupId = student.GroupId
            };

            await _repositoryManager.Student.AddAsync(newStudent, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var Student = await _repositoryManager.Student.GetByIdAsync(id, cancellationToken);

            if (Student is null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found. It is possible that someone else deleted this student.");
            }

            _repositoryManager.Student.Remove(Student, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(StudentToUpdateDTO student, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(student, nameof(student));

            var StudentToUpdate = await _repositoryManager.Student.GetByIdAsync(student.Id, cancellation);

            if (StudentToUpdate is null)
            {
                throw new KeyNotFoundException($"Student with id {student.Id} not found. It is possible that someone else deleted this student.");
            }

            StudentToUpdate.FirstName = student.FirstName;
            StudentToUpdate.LastName = student.LastName;
            StudentToUpdate.GroupId = student.GroupId;

            _repositoryManager.Student.Update(StudentToUpdate, cancellation);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellation);
        }

        public async Task<bool> CanBeCreatedAsync(CancellationToken cancellationToken = default)
        {
            return (await _repositoryManager.Group.GetAllAsync(cancellationToken)).Any();
        }
    }
}
