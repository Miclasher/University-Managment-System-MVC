using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Shared
{
    public class TeacherDTO
    {
        public Guid Id { get; set; }
        [DisplayName("Name")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter teacher's name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Surname")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter teacher's surname")]
        public string LastName { get; set; } = string.Empty;
        public IEnumerable<GroupDTO> Groups { get; set; } = new List<GroupDTO>();

        public string FullName => $"{FirstName} {LastName}";
    }
}
