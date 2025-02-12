using University.Shared;

namespace University.UI.ViewModels
{
    public sealed class GroupIndexViewModel : BaseIndexViewModel
    {
        public IEnumerable<GroupDTO> Groups { get; set; } = new List<GroupDTO>();
    }
}
