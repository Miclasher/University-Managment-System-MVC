namespace University.Domain.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public IEnumerable<Group> Groups { get; set; } = new List<Group>();
    }
}
