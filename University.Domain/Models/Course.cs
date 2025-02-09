namespace University.Domain.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<Group> Groups { get; set; } = new List<Group>();
    }
}
