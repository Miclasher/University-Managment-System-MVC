using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using University.Domain.Models;
using University.Services;
using University.Shared;

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
        public async Task LoadViewDataForStudentsTest1Async()
        {
            var groups = new List<Group>
            {
                new Group { Id = Guid.NewGuid(), Name = "Group1", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid() },
                new Group { Id = Guid.NewGuid(), Name = "Group2", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid() }
            };

            _mockGroupRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(groups);

            var result = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

            await _viewDataService.LoadViewDataForStudents(result);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, ((IEnumerable<GroupDTO>)result["Groups"]).Count());
        }

        [TestMethod]
        public async Task LoadViewDataForGroupsTest1Async()
        {
            var courses = new List<Course>
            {
                new Course { Id = Guid.NewGuid(), Name = "Course1", Description = "Description1" },
                new Course { Id = Guid.NewGuid(), Name = "Course2", Description = "Description2" }
            };

            var teachers = new List<Teacher>
            {
                new Teacher { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Teacher { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
            };

            _mockCourseRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(courses);
            _mockTeacherRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(teachers);

            var result = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

            await _viewDataService.LoadViewDataForGroups(result);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, ((IEnumerable<CourseDTO>)result["Courses"]).Count());
            Assert.AreEqual(2, ((IEnumerable<TeacherDTO>)result["Teachers"]).Count());
        }
    }
}
