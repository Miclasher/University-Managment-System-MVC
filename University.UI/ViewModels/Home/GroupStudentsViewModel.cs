using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class GroupStudentsViewModel
    {
        public Guid CourseId { get; set; }
        public IEnumerable<StudentDTO> Students { get; set; } = new List<StudentDTO>();
    }
}
