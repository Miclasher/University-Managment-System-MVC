using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.DataLayer.Models
{
    public class Group
    {
        [Column("GROUP_ID")]
        public Guid Id { get; set; }
        [Column("NAME")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter group name")]
        public string Name { get; set; } = string.Empty;
        [Column("COURSE_ID")]
        [Required(ErrorMessage = "Please select a course")]
        public Guid CourseId { get; set; }
        [ValidateNever]
        public Course Course { get; set; } = null!;
        [Column("TEACHER_ID")]
        [Required(ErrorMessage = "Please select a tutor for a group")]
        public Guid TeacherId { get; set; }
        [DisplayName("Tutor")]
        [ValidateNever]
        public Teacher Teacher { get; set; } = null!;
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
    }
}
