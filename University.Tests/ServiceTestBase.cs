using Moq;
using University.Domain.Repositories;
using University.Services.Abstractions;

namespace University.Tests
{
    public class ServiceTestBase
    {
        protected readonly Mock<IRepositoryManager> _mockRepositoryManager;
        protected readonly Mock<IStudentRepository> _mockStudentRepository;
        protected readonly Mock<IGroupRepository> _mockGroupRepository;
        protected readonly Mock<ICourseRepository> _mockCourseRepository;
        protected readonly Mock<ITeacherRepository> _mockTeacherRepository;
        protected readonly Mock<IUnitOfWork> _mockUnitOfWork;
        public ServiceTestBase()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockGroupRepository = new Mock<IGroupRepository>();
            _mockCourseRepository = new Mock<ICourseRepository>();
            _mockTeacherRepository = new Mock<ITeacherRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockRepositoryManager.Setup(r => r.Student).Returns(_mockStudentRepository.Object);
            _mockRepositoryManager.Setup(r => r.Group).Returns(_mockGroupRepository.Object);
            _mockRepositoryManager.Setup(r => r.Course).Returns(_mockCourseRepository.Object);
            _mockRepositoryManager.Setup(r => r.Teacher).Returns(_mockTeacherRepository.Object);
            _mockRepositoryManager.Setup(r => r.UnitOfWork).Returns(_mockUnitOfWork.Object);
        }
    }
}
