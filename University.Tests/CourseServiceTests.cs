using Moq;
using University.Domain.Models;
using University.Services;
using University.Shared;

namespace University.Tests
{
    [TestClass]
    public class CourseServiceTests : ServiceTestBase
    {
        private readonly CourseService _courseService;

        public CourseServiceTests() : base()
        {
            _courseService = new CourseService(_mockRepositoryManager.Object);
        }

        [TestMethod]
        public async Task GetAllAsyncTest1()
        {
            var courses = new List<Course>
            {
                new Course { Id = Guid.NewGuid(), Name = "Course1", Description = "Description1" },
                new Course { Id = Guid.NewGuid(), Name = "Course2", Description = "Description2" }
            };

            _mockCourseRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(courses);

            var result = await _courseService.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsyncTest1()
        {
            var courseId = Guid.NewGuid();
            var course = new Course { Id = courseId, Name = "Course1", Description = "Description1" };

            _mockCourseRepository.Setup(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(course);

            var result = await _courseService.GetByIdAsync(courseId);

            Assert.IsNotNull(result);
            Assert.AreEqual(courseId, result.Id);
        }

        [TestMethod]
        public async Task CreateAsyncTest1()
        {
            var courseToCreate = new CourseToCreateDTO
            {
                Name = "Course1",
                Description = "Description1"
            };

            await _courseService.CreateAsync(courseToCreate);

            _mockCourseRepository.Verify(repo => repo.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest1()
        {
            var courseId = Guid.NewGuid();
            var course = new Course { Id = courseId, Name = "Course1", Description = "Description1", Groups = new List<Group>() };

            _mockCourseRepository.Setup(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(course);

            await _courseService.DeleteAsync(courseId);

            _mockCourseRepository.Verify(repo => repo.Remove(course, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            var courseId = Guid.NewGuid();
            var course = new Course { Id = courseId, Name = "Course1", Description = "Description1", Groups = new List<Group> { new Group() } };

            _mockCourseRepository.Setup(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(course);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _courseService.DeleteAsync(courseId));
        }

        [TestMethod]
        public async Task UpdateAsyncTest1()
        {
            var courseId = Guid.NewGuid();
            var courseToUpdate = new CourseToUpdateDTO
            {
                Id = courseId,
                Name = "UpdatedCourse",
                Description = "UpdatedDescription"
            };

            var course = new Course { Id = courseId, Name = "OldCourse", Description = "OldDescription" };

            _mockCourseRepository.Setup(repo => repo.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(course);

            await _courseService.UpdateAsync(courseToUpdate);

            _mockCourseRepository.Verify(repo => repo.Update(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetCourseWithGroupDetailsByIdAsyncTest1()
        {
            var courseId = Guid.NewGuid();
            var course = new Course { Id = courseId, Name = "Course1", Description = "Description1", Groups = new List<Group> { new Group() } };

            _mockCourseRepository.Setup(repo => repo.GetCourseWithGroupDetailsByIdAsync(courseId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(course);

            var result = await _courseService.GetCourseWithGroupDetailsByIdAsync(courseId);

            Assert.IsNotNull(result);
            Assert.AreEqual(courseId, result.Id);
        }

        [TestMethod]
        public async Task InvalidIdTest()
        {
            _mockCourseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Course)null!);

            var courseToUpdate = new CourseToUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = "UpdatedCourse",
                Description = "UpdatedDescription"
            };

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _courseService.DeleteAsync(Guid.NewGuid()));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _courseService.UpdateAsync(courseToUpdate));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _courseService.GetByIdAsync(Guid.NewGuid()));
        }

        [TestMethod]
        public async Task ArgumentNullTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _courseService.CreateAsync(null!));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _courseService.UpdateAsync(null!));
        }
    }
}
