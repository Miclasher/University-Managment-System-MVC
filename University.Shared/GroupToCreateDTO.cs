using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Shared
{
    public class GroupToCreateDTO
    {
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter group name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select a course")]
        [DisplayName("Course")]
        public Guid CourseId { get; set; }
        [DisplayName("Teacher")]
        [Required(ErrorMessage = "Please select a tutor for a group")]
        public Guid TeacherId { get; set; }
    }
}
