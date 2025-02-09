using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Shared
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter group name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select a course")]
        public Guid CourseId { get; set; }
        public CourseDTO Course { get; set; } = null!;
        [Required(ErrorMessage = "Please select a tutor for a group")]
        public Guid TeacherId { get; set; }
        [DisplayName("Tutor")]
        public TeacherDTO Teacher { get; set; } = null!;
        public IEnumerable<StudentDTO> Students { get; set; } = new List<StudentDTO>();
    }
}
