using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Group
    {
        [Column("GROUP_ID")]
        public Guid Id { get; set; }
        [Column("NAME")]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        [Column("COURSE_ID")]
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
        [Column("TEACHER_ID")]
        public Guid TeacherId { get; set; }
        [DisplayName("Tutor")]
        public Teacher Teacher { get; set; } = null!;
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
    }
}
