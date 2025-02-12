using Microsoft.AspNetCore.Mvc.ViewFeatures;
using University.Shared;

namespace University.Services.Abstractions
{
    public interface IViewDataService
    {
        Task<IEnumerable<GroupDTO>> LoadViewDataForStudents();
        Task<IEnumerable<CourseDTO>> LoadCoursesDataForGroups();
        Task<IEnumerable<TeacherDTO>> LoadTeachersDataForGroups();
    }
}
