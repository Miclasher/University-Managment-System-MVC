using University.Shared;

namespace University.UI.ViewModels
{
    public class CourseIndexViewModel : BaseIndexViewModel
    {
        public IEnumerable<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
    }
}
