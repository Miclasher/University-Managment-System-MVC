using Moq;
using University.Domain.Models;
using University.Services;
using University.Shared;

namespace University.Tests
{
    [TestClass]
    public class GroupServiceTests : ServiceTestBase
    {
        private readonly GroupService _groupService;

        public GroupServiceTests() : base()
        {
            _groupService = new GroupService(_mockRepositoryManager.Object);
        }

        [TestMethod]
        public async Task GetAllAsyncTest1()
        {
            var groups = new List<Group>
            {
                new Group { Id = Guid.NewGuid(), Name = "Group1", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid() },
                new Group { Id = Guid.NewGuid(), Name = "Group2", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid() }
            };

            _mockGroupRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(groups);

            var result = await _groupService.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsyncTest1()
        {
            var groupId = Guid.NewGuid();
            var group = new Group { Id = groupId, Name = "Group1", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid() };

            _mockGroupRepository.Setup(repo => repo.GetByIdAsync(groupId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

            var result = await _groupService.GetByIdAsync(groupId);

            Assert.IsNotNull(result);
            Assert.AreEqual(groupId, result.Id);
        }

        [TestMethod]
        public async Task CreateAsyncTest1()
        {
            var groupToCreate = new GroupToCreateDTO
            {
                Name = "Group1",
                CourseId = Guid.NewGuid(),
                TeacherId = Guid.NewGuid()
            };

            await _groupService.CreateAsync(groupToCreate);

            _mockGroupRepository.Verify(repo => repo.AddAsync(It.IsAny<Group>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest1()
        {
            var groupId = Guid.NewGuid();
            var group = new Group { Id = groupId, Name = "Group1", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid(), Students = new List<Student>() };

            _mockGroupRepository.Setup(repo => repo.GetByIdAsync(groupId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

            await _groupService.DeleteAsync(groupId);

            _mockGroupRepository.Verify(repo => repo.Remove(group, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            var groupId = Guid.NewGuid();
            var group = new Group { Id = groupId, Name = "Group1", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid(), Students = new List<Student> { new Student() } };

            _mockGroupRepository.Setup(repo => repo.GetByIdAsync(groupId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _groupService.DeleteAsync(groupId));
        }

        [TestMethod]
        public async Task UpdateAsyncTest1()
        {
            var groupId = Guid.NewGuid();
            var groupToUpdate = new GroupToUpdateDTO
            {
                Id = groupId,
                Name = "UpdatedGroup",
                CourseId = Guid.NewGuid(),
                TeacherId = Guid.NewGuid()
            };

            var group = new Group { Id = groupId, Name = "OldGroup", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid() };

            _mockGroupRepository.Setup(repo => repo.GetByIdAsync(groupId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

            await _groupService.UpdateAsync(groupToUpdate);

            _mockGroupRepository.Verify(repo => repo.Update(It.IsAny<Group>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CanBeCreatedAsyncTest1()
        {
            var courses = new List<Course>
            {
                new Course { Id = Guid.NewGuid(), Name = "Course1", Description = "Description1" }
            };

            var teachers = new List<Teacher>
            {
                new Teacher { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" }
            };

            _mockCourseRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(courses);

            _mockTeacherRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(teachers);

            var result = await _groupService.CanBeCreatedAsync();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ClearGroupAsyncTest1()
        {
            var groupId = Guid.NewGuid();
            var group = new Group { Id = groupId, Name = "Group1", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid(), Students = new List<Student> { new Student() } };

            _mockGroupRepository.Setup(repo => repo.GetByIdAsync(groupId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

            await _groupService.ClearGroupAsync(groupId);

            _mockGroupRepository.Verify(repo => repo.DeleteStudentsFromGroup(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task InvalidIdTest()
        {
            _mockGroupRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Group)null!);

            var groupToUpdate = new GroupToUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = "UpdatedGroup",
                CourseId = Guid.NewGuid(),
                TeacherId = Guid.NewGuid()
            };

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _groupService.DeleteAsync(Guid.NewGuid()));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _groupService.UpdateAsync(groupToUpdate));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _groupService.ClearGroupAsync(Guid.NewGuid()));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _groupService.GetByIdAsync(Guid.NewGuid()));
        }

        [TestMethod]
        public async Task ArgumentNullTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _groupService.CreateAsync(null!));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _groupService.UpdateAsync(null!));
        }
    }
}
