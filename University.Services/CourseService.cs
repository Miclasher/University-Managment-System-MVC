using Mapster;
using University.Domain.Models;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    internal sealed class CourseService : ICourseService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CourseService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var courses = await _repositoryManager.Course.GetAllAsync(cancellationToken);

            return courses.Adapt<IEnumerable<CourseDTO>>();
        }

        public async Task<CourseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var course = await _repositoryManager.Course.GetByIdAsync(id, cancellationToken);

            if (course is null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found");
            }

            return course.Adapt<CourseDTO>();
        }

        public async Task CreateAsync(CourseToCreateDTO course, CancellationToken cancellationToken = default)
        {
            var newCourse = new Course()
            {
                Id = Guid.NewGuid(),
                Name = course.Name,
                Description = course.Description
            };

            await _repositoryManager.Course.AddAsync(newCourse, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var course = await _repositoryManager.Course.GetByIdAsync(id, cancellationToken);

            if (course is null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found");
            }

            _repositoryManager.Course.Remove(course, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CourseToUpdateDTO course, CancellationToken cancellationToken = default)
        {
            var courseToUpdate = await _repositoryManager.Course.GetByIdAsync(course.Id, cancellationToken);

            courseToUpdate.Name = course.Name;
            courseToUpdate.Description = course.Description;

            await _repositoryManager.Course.UpdateAsync(courseToUpdate, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
