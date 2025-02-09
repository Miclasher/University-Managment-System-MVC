using System.ComponentModel.DataAnnotations;

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
        public IEnumerable<GroupDTO> Groups { get; set; } = new List<GroupDTO>();
    }
}
