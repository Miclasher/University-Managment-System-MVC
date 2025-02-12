using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class GroupDeleteViewModel : BaseGroupActionsViewModel
    {
        public GroupDTO Group { get; set; } = new GroupDTO();
    }
}
