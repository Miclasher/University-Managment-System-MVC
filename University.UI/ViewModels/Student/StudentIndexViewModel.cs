using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class StudentIndexViewModel : BaseIndexViewModel
    {
        public IEnumerable<StudentDTO> Students { get; set; } = new List<StudentDTO>();
        public bool CanBeCreated { get; set; }
    }
}
