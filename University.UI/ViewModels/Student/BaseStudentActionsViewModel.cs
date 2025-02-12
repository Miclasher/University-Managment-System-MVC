using University.Shared;

namespace University.UI.ViewModels
{
    public abstract class BaseStudentActionsViewModel
    {
        public IEnumerable<GroupDTO> Groups { get; set; } = new List<GroupDTO>();
    }
}
