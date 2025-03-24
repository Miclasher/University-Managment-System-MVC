using University.Shared;

namespace University.UI.ViewModels
{
    public abstract class BaseGroupActionsViewModel
    {
        public IEnumerable<TeacherDTO> Teachers { get; set; } = new List<TeacherDTO>();
        public IEnumerable<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
    }
}
