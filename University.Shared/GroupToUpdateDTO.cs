using System.ComponentModel.DataAnnotations;

namespace University.Shared
{
    public class GroupToUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter group name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select a course")]
        public Guid CourseId { get; set; }
        [Required(ErrorMessage = "Please select a tutor for a group")]
        public Guid TeacherId { get; set; }
    }
}
