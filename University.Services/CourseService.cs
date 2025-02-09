using Mapster;
using University.Domain.Models;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    internal sealed class CourseService : ICourseService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CourseService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var courses = await _repositoryWrapper.Course.GetAllAsync(cancellationToken);

            return courses.Adapt<IEnumerable<CourseDTO>>();
        }

        public async Task<CourseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var course = await _repositoryWrapper.Course.GetByIdAsync(id, cancellationToken);

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
                Name = course.Name,
                Description = course.Description
            };

            await _repositoryWrapper.Course.AddAsync(newCourse, cancellationToken);

            await _repositoryWrapper.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var course = await _repositoryWrapper.Course.GetByIdAsync(id, cancellationToken);

            if (course is null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found");
            }

            _repositoryWrapper.Course.Remove(course, cancellationToken);

            await _repositoryWrapper.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CourseToUpdateDTO course, CancellationToken cancellationToken = default)
        {
            var courseToUpdate = await _repositoryWrapper.Course.GetByIdAsync(course.Id, cancellationToken);

            courseToUpdate.Name = course.Name;
            courseToUpdate.Description = course.Description;

            await _repositoryWrapper.Course.UpdateAsync(courseToUpdate, cancellationToken);

            await _repositoryWrapper.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
