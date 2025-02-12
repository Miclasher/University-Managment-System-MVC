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

        public async Task<IEnumerable<CourseDTO>> LoadCoursesDataForGroups()
        {
            return (await _repositoryManager.Course.GetAllAsync()).Adapt<IEnumerable<CourseDTO>>();
        }

        public async Task<IEnumerable<TeacherDTO>> LoadTeachersDataForGroups()
        {
            return (await _repositoryManager.Teacher.GetAllAsync()).Adapt<IEnumerable<TeacherDTO>>();
        }
    }
}
