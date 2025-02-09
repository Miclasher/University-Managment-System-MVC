namespace University.Services.Abstractions
{
    public interface IServiceManager
    {
        public ICourseService CourseService { get; }
        public ITeacherService TeacherService { get; }
        public IGroupService GroupService { get; }
        public IStudentService StudentService { get; }
    }
}
