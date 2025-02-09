using University.Domain.Repositories;
using University.Services.Abstractions;

namespace University.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICourseService> _lazyCourseService;
        private readonly Lazy<ITeacherService> _lazyTeacherService;
        private readonly Lazy<IStudentService> _lazyStudentService;
        private readonly Lazy<IGroupService> _lazyGroupService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyCourseService = new Lazy<ICourseService>(() => new CourseService(repositoryManager));
            _lazyTeacherService = new Lazy<ITeacherService>(() => new TeacherService(repositoryManager));
            _lazyStudentService = new Lazy<IStudentService>(() => new StudentService(repositoryManager));
            _lazyGroupService = new Lazy<IGroupService>(() => new GroupService(repositoryManager));
        }

        public ICourseService CourseService => _lazyCourseService.Value;

        public ITeacherService TeacherService => _lazyTeacherService.Value;

        public IStudentService StudentService => _lazyStudentService.Value;

        public IGroupService GroupService => _lazyGroupService.Value;
    }
}
