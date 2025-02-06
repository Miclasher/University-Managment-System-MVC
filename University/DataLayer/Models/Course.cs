using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.DataLayer.Models
{
    public class Course
    {
        [Column("COURSE_ID")]
        public Guid Id { get; set; }
        [Column("NAME")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter course name")]
        public string Name { get; set; } = string.Empty;
        [Column("DESCRIPTION")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter course description")]
        public string Description { get; set; } = string.Empty;
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
    }
}
