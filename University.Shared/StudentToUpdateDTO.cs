using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace University.Shared
{
    public class StudentToUpdateDTO
    {
        public Guid Id { get; set; }
        [DisplayName("Name")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter student's name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Surname")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter student's surname")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select a group")]
        [DisplayName("Group")]
        public Guid GroupId { get; set; }
    }
}
