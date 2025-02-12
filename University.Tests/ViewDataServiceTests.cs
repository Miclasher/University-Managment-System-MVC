using Moq;
using University.Domain.Models;
using University.Services;

namespace University.Tests
{
    [TestClass]
    public class ViewDataServiceTests : ServiceTestBase
    {
        private readonly ViewDataService _viewDataService;

        public ViewDataServiceTests() : base()
        {
            _viewDataService = new ViewDataService(_mockRepositoryManager.Object);
        }

        [TestMethod]
        public async Task LoadViewDataForStudentsTest()
        {
            var groups = new List<Group>
            {
                new Group { Id = Guid.NewGuid(), Name = "Group1" },
                new Group { Id = Guid.NewGuid(), Name = "Group2" }
            };

            _mockGroupRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(groups);

            var result = await _viewDataService.LoadViewDataForStudents();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task LoadCoursesDataForGroupsTest()
        {
            var courses = new List<Course>
            {
                new Course { Id = Guid.NewGuid(), Name = "Course1", Description = "Description1" },
                new Course { Id = Guid.NewGuid(), Name = "Course2", Description = "Description2" }
            };

            _mockCourseRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(courses);

            var result = await _viewDataService.LoadCoursesDataForGroups();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task LoadTeachersDataForGroupsTest()
        {
            var teachers = new List<Teacher>
            {
                new Teacher { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Teacher { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
            };

            _mockTeacherRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(teachers);

            var result = await _viewDataService.LoadTeachersDataForGroups();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
