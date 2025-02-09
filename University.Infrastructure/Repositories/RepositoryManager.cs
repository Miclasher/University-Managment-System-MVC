using University.Domain.Repositories;

namespace University.Infrastructure.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly UniversityDbContext _context;
        private readonly Lazy<IGroupRepository> _lazyGroupRepository;
        private readonly Lazy<IStudentRepository> _lazyStudentRepository;
        private readonly Lazy<ITeacherRepository> _lazyTeacherRepository;
        private readonly Lazy<ICourseRepository> _lazyCourseRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(UniversityDbContext context)
        {
            _context = context;
            _lazyGroupRepository = new Lazy<IGroupRepository>(() => new GroupRepository(_context));
            _lazyStudentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(_context));
            _lazyTeacherRepository = new Lazy<ITeacherRepository>(() => new TeacherRepository(_context));
            _lazyCourseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_context));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(_context));
        }

        public IGroupRepository Group => _lazyGroupRepository.Value;

        public IStudentRepository Student => _lazyStudentRepository.Value;

        public ITeacherRepository Teacher => _lazyTeacherRepository.Value;

        public ICourseRepository Course => _lazyCourseRepository.Value;

        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
