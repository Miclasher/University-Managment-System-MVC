using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Teacher
    {
        [Column("TEACHER_ID")]
        public Guid Id { get; set; }
        [Column("FIRST_NAME")]
        [DisplayName("Name")]
        [MinLength(1)]
        public string FirstName { get; set; } = string.Empty;
        [Column("LAST_NAME")]
        [DisplayName("Surname")]
        [MinLength(1)]
        public string LastName { get; set; } = string.Empty;
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();

        public string FullName => $"{FirstName} {LastName}";
    }
}
