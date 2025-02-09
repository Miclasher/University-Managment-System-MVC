namespace University.Domain.Repositories
{
    public interface IRepositoryWrapper
    {
        IGroupRepository Group { get; }
        IStudentRepository Student { get; }
        ITeacherRepository Teacher { get; }
        ICourseRepository Course { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
