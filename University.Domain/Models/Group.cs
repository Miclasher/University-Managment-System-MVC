using System.Collections.ObjectModel;

namespace University.Domain.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
    }
}
