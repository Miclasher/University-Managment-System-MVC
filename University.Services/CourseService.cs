﻿using Mapster;
using University.Domain.Models;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    public sealed class CourseService : ICourseService
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
            ArgumentNullException.ThrowIfNull(course, nameof(course));

            var newCourse = course.Adapt<Course>();

            await _repositoryManager.Course.AddAsync(newCourse, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var course = await _repositoryManager.Course.GetByIdAsync(id, cancellationToken);

            if (course is null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found. It is possible that someone else deleted this course.");
            }

            if (course.Groups.Any())
            {
                throw new InvalidOperationException("Course cannot be deleted because it has groups.");
            }

            _repositoryManager.Course.Remove(course, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CourseToUpdateDTO course, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(course, nameof(course));

            var courseToUpdate = await _repositoryManager.Course.GetByIdAsync(course.Id, cancellationToken);

            if (courseToUpdate is null)
            {
                throw new KeyNotFoundException($"Course with id {course.Id} not found. It is possible that someone else deleted this course.");
            }

            courseToUpdate.Name = course.Name;
            courseToUpdate.Description = course.Description;

            _repositoryManager.Course.Update(courseToUpdate, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<CourseDTO> GetCourseWithGroupDetailsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var course = await _repositoryManager.Course.GetCourseWithGroupDetailsByIdAsync(id, cancellationToken);

            if (course is null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found. It is possible that someone else deleted this course.");
            }

            return course.Adapt<CourseDTO>();
        }
    }
}
