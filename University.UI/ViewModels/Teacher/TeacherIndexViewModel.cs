using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class TeacherIndexViewModel : BaseIndexViewModel
    {
        public IEnumerable<TeacherDTO> Teachers { get; set; } = new List<TeacherDTO>();
    }
}
