using Mapster;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    public sealed class ViewDataService : IViewDataService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ViewDataService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<GroupDTO>> LoadViewDataForStudents()
        {
            return (await _repositoryManager.Group.GetAllAsync()).Adapt<IEnumerable<GroupDTO>>();
        }

        public async Task LoadViewDataForGroups(IEnumerable<CourseDTO> courses, IEnumerable<TeacherDTO> teachers)
        {
            ArgumentNullException.ThrowIfNull(courses, nameof(courses));
            ArgumentNullException.ThrowIfNull(teachers, nameof(teachers));

            courses = (await _repositoryManager.Course.GetAllAsync()).Adapt<IEnumerable<CourseDTO>>();
            teachers = (await _repositoryManager.Teacher.GetAllAsync()).Adapt<IEnumerable<TeacherDTO>>();
        }
    }
}
