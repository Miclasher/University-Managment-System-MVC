using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class GroupEditViewModel : BaseGroupActionsViewModel
    {
        public GroupToUpdateDTO Group { get; set; } = new GroupToUpdateDTO();
    }
}
