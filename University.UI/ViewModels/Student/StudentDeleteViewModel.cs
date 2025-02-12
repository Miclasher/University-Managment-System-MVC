using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class StudentDeleteViewModel : BaseStudentActionsViewModel
    {
        public StudentDTO Student { get; set; } = new StudentDTO();
    }
}
