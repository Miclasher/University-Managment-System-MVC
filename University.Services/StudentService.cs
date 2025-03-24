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
            var students = await _repositoryManager.Student.GetAllAsync(cancellationToken);

            return students.Adapt<IEnumerable<StudentDTO>>();
        }

        public async Task<StudentDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var student = await _repositoryManager.Student.GetByIdAsync(id, cancellationToken);

            if (student is null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found. It is possible that someone else deleted this student.");
            }

            return student.Adapt<StudentDTO>();
        }

        public async Task CreateAsync(StudentToCreateDTO student, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(student, nameof(student));

            var newStudent = student.Adapt<Student>();

            await _repositoryManager.Student.AddAsync(newStudent, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var student = await _repositoryManager.Student.GetByIdAsync(id, cancellationToken);

            if (student is null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found. It is possible that someone else deleted this student.");
            }

            _repositoryManager.Student.Remove(student, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(StudentToUpdateDTO student, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(student, nameof(student));

            var studentToUpdate = await _repositoryManager.Student.GetByIdAsync(student.Id, cancellation);

            if (studentToUpdate is null)
            {
                throw new KeyNotFoundException($"Student with id {student.Id} not found. It is possible that someone else deleted this student.");
            }

            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.GroupId = student.GroupId;

            _repositoryManager.Student.Update(studentToUpdate, cancellation);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellation);
        }

        public async Task<bool> CanBeCreatedAsync(CancellationToken cancellationToken = default)
        {
            return (await _repositoryManager.Group.GetAllAsync(cancellationToken)).Any();
        }
    }
}
