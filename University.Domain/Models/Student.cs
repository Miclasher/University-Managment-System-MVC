namespace University.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}
