using Microsoft.AspNetCore.Mvc.ViewFeatures;
using University.Shared;

namespace University.Services.Abstractions
{
    public interface IViewDataService
    {
        Task<IEnumerable<GroupDTO>> LoadViewDataForStudents();
        Task LoadViewDataForGroups(IEnumerable<CourseDTO> courses, IEnumerable<TeacherDTO> teachers);
    }
}
