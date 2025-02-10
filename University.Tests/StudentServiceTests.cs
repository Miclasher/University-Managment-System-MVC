using Moq;
using University.Domain.Models;
using University.Services;
using University.Shared;

namespace University.Tests
{
    [TestClass]
    public class StudentServiceTests : ServiceTestBase
    {
        private readonly StudentService _studentService;

        public StudentServiceTests() : base()
        {
            _studentService = new StudentService(_mockRepositoryManager.Object);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest1()
        {
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, FirstName = "John", LastName = "Doe", GroupId = Guid.NewGuid() };

            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(student);

            var result = await _studentService.GetByIdAsync(studentId);

            Assert.IsNotNull(result);
            Assert.AreEqual(studentId, result.Id);
        }

        [TestMethod]
        public async Task GetAllAsyncTest1()
        {
            var students = new List<Student>
            {
                new Student { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", GroupId = Guid.NewGuid() },
                new Student { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", GroupId = Guid.NewGuid() }
            };

            _mockStudentRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(students);

            var result = await _studentService.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task CreateAsyncTest1()
        {
            var studentToCreate = new StudentToCreateDTO
            {
                FirstName = "John",
                LastName = "Doe",
                GroupId = Guid.NewGuid()
            };

            await _studentService.CreateAsync(studentToCreate);

            _mockStudentRepository.Verify(repo => repo.AddAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest1()
        {
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, FirstName = "John", LastName = "Doe", GroupId = Guid.NewGuid() };

            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(student);

            await _studentService.DeleteAsync(studentId);

            _mockStudentRepository.Verify(repo => repo.Remove(student, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest1()
        {
            var studentId = Guid.NewGuid();
            var studentToUpdate = new StudentToUpdateDTO
            {
                Id = studentId,
                FirstName = "John",
                LastName = "Doe",
                GroupId = Guid.NewGuid()
            };

            var student = new Student { Id = studentId, FirstName = "OldFirstName", LastName = "OldLastName", GroupId = Guid.NewGuid() };

            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(student);

            await _studentService.UpdateAsync(studentToUpdate);

            _mockStudentRepository.Verify(repo => repo.Update(It.IsAny<Student>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositoryManager.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CanBeCreatedAsyncTest1()
        {
            var groups = new List<Group>
            {
                new Group { Id = Guid.NewGuid(), Name = "Group1", CourseId = Guid.NewGuid(), TeacherId = Guid.NewGuid() }
            };

            _mockGroupRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(groups);

            var result = await _studentService.CanBeCreatedAsync();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task InvalidIdTest()
        {
            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Student)null!);

            var studentToUpdate = new StudentToUpdateDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                GroupId = Guid.NewGuid()
            };

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _studentService.DeleteAsync(Guid.NewGuid()));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _studentService.UpdateAsync(studentToUpdate));
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _studentService.GetByIdAsync(Guid.NewGuid()));
        }

    }
}
