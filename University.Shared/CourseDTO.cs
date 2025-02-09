using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace University.Shared
{
    public class CourseDTO
    {
        [Required]
        public Guid Id { get; set; }
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter course name")]
        public string Name { get; set; } = string.Empty;
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter course description")]
        public string Description { get; set; } = string.Empty;
        public ObservableCollection<GroupDTO> Groups { get; set; } = new ObservableCollection<Group>();
    }
}
