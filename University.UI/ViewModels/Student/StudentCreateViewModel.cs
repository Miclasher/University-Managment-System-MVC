using University.Shared;

namespace University.UI.ViewModels
{
    public class StudentCreateViewModel : BaseStudentActionsViewModel
    {
        public StudentToCreateDTO Student { get; set; } = new StudentToCreateDTO();
    }
}
