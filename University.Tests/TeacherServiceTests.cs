using Moq;
using University.Domain.Models;
using University.Services;
using University.Shared;

namespace University.Tests
{
    [TestClass]
    public class TeacherServiceTests : ServiceTestBase
    {
        private readonly TeacherService _teacherService;

        public TeacherServiceTests() : base()
        {
            _teacherService = new TeacherService(_mockRepositoryManager.Object);
        }

        [TestMethod]
        public async Task GetAllAsyncTest1()
        {
            var teachers = new List<Teacher>
            {
                new Teacher { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Teacher { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
            };

            _mockTeacherRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(teachers);

            var result = await _teacherService.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsyncTest1()
        {
            var teacherId = Guid.NewGuid();
            var teacher = new Teacher { Id = teacherId, FirstName = "John", LastName = "Doe" };

            _mockTeacherRepository.Setup(repo => repo.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(teacher);

            var result = await _teacherService.GetByIdAsync(teacherId);

            Assert.IsNotNull(result);
            Assert.AreEqual(teacherId, result.Id);
        }

        [TestMethod]
        public async Task CreateAsyncTest1()
        {
            var teacherToCreate = new TeacherToCreateDTO
            {
                FirstName = "John",
                LastName = "Doe"
            };

            await _teacherService.CreateAsync(teacherToCreate);

            _mockTeacherRepository.Verify(repo => repo.AddAsync(It.IsAny<Teacher>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest1()
        {
            var teacherId = Guid.NewGuid();
            var teacher = new Teacher { Id = teacherId, FirstName = "John", LastName = "Doe", Groups = new List<Group>() };

            _mockTeacherRepository.Setup(repo => repo.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(teacher);

            await _teacherService.DeleteAsync(teacherId);

            _mockTeacherRepository.Verify(repo => repo.Remove(teacher, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            var teacherId = Guid.NewGuid();
            var teacher = new Teacher { Id = teacherId, FirstName = "John", LastName = "Doe", Groups = new List<Group> { new Group() } };

            _mockTeacherRepository.Setup(repo => repo.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(teacher);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _teacherService.DeleteAsync(teacherId));
        }

        [TestMethod]
        public async Task UpdateAsyncTest1()
        {
            var teacherId = Guid.NewGuid();
            var teacherToUpdate = new TeacherToUpdateDTO
            {
                Id = teacherId,
                FirstName = "John",
                LastName = "Doe"
            };

            var teacher = new Teacher { Id = teacherId, FirstName = "OldFirstName", LastName = "OldLastName" };

            _mockTeacherRepository.Setup(repo => repo.GetByIdAsync(teacherId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(teacher);

            await _teacherService.UpdateAsync(teacherToUpdate);

            _mockTeacherRepository.Verify(repo => repo.Update(It.IsAny<Teacher>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
