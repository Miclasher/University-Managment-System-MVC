using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class HomeIndexViewModel : BaseIndexViewModel
    {
        public IEnumerable<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
    }
}
