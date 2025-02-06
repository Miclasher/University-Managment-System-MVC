using Microsoft.AspNetCore.Mvc;
using University.Controllers;
using University.Models;

namespace University.Tests
{
    [TestClass]
    public class StudentsControllerTests : CrudControllerTestSetup
    {
        private StudentsController _controller = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _controller = new StudentsController(Context);
        }

        [TestMethod]
        public async Task IndexAsyncTest()
        {
            var result = await _controller.IndexAsync();
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(IEnumerable<Student>));
            Assert.AreEqual(3, (model as IEnumerable<Student>)!.Count());
        }

        [TestMethod]
        public async Task CreateAsyncTest()
        {
            var newStudent = new Student { Id = Guid.NewGuid(), FirstName = "New", LastName = "Student", GroupId = Context.Groups.First().Id };

            var result = await _controller.CreateAsync(newStudent);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(4, Context.Students.Count());
        }

        [TestMethod]
        public async Task EditAsyncTest()
        {
            var student = Context.Students.First();

            var result = await _controller.EditAsync(student.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Student));
            Assert.AreEqual(student.Id, (model as Student)!.Id);
        }

        [TestMethod]
        public async Task EditAsyncTest2()
        {
            var student = Context.Students.First();

            student.FirstName = "Edited";

            await _controller.EditAsync(student);

            Assert.AreEqual<Student>(student, await Context.Students.FindAsync(student.Id));
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            var student = Context.Students.First();

            var result = await _controller.DeleteAsync(student.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Student));
            Assert.AreEqual(student.Id, (model as Student)!.Id);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            var student = Context.Students.First();

            var result = await _controller.DeleteAsync(student);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(2, Context.Students.Count());
        }
    }
}
