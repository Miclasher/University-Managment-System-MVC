using Mapster;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using University.Domain.Repositories;
using University.Services.Abstractions;
using University.Shared;

namespace University.Services
{
    internal sealed class ViewDataService : IViewDataService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ViewDataService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task LoadViewDataForStudents(ViewDataDictionary viewData)
        {
            viewData["Groups"] = (await _repositoryManager.Group.GetAllAsync()).Adapt<IEnumerable<GroupDTO>>();
        }

        public async Task LoadViewDataForGroups(ViewDataDictionary viewData)
        {
            viewData["Courses"] = (await _repositoryManager.Course.GetAllAsync()).Adapt<IEnumerable<CourseDTO>>();
            viewData["Teachers"] = (await _repositoryManager.Teacher.GetAllAsync()).Adapt<IEnumerable<TeacherDTO>>();
        }
    }
}
