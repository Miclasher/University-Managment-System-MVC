using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class GroupCreateViewModel : BaseGroupActionsViewModel
    {
        public GroupToCreateDTO Group { get; set; } = new GroupToCreateDTO();
    }
}
