using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class StudentEditViewModel : BaseStudentActionsViewModel
    {
        public StudentToUpdateDTO Student { get; set; } = new StudentToUpdateDTO();
    }
}
