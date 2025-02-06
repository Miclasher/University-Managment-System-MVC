using Microsoft.AspNetCore.Mvc;
using University.Controllers;
using University.Models;

namespace University.Tests
{
    [TestClass]
    public class TeachersControllerTests : CrudControllerTestSetup
    {
        private TeachersController _controller = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _controller = new TeachersController(Context);
        }

        [TestMethod]
        public async Task IndexAsyncTest()
        {
            var result = await _controller.IndexAsync();
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(IEnumerable<Teacher>));
            Assert.AreEqual(1, (model as IEnumerable<Teacher>)!.Count());
        }

        [TestMethod]
        public async Task CreateAsyncTest()
        {
            var newTeacher = new Teacher { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Johnson" };

            var result = await _controller.CreateAsync(newTeacher);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(2, Context.Teachers.Count());
        }

        [TestMethod]
        public async Task EditAsyncTest()
        {
            var teacher = Context.Teachers.First();

            var result = await _controller.EditAsync(teacher.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Teacher));
            Assert.AreEqual(teacher.Id, (model as Teacher)!.Id);
        }

        [TestMethod]
        public async Task EditAsyncTest2()
        {
            var teacher = Context.Teachers.First();

            teacher.FirstName = "Edited";

            await _controller.EditAsync(teacher);

            Assert.AreEqual<Teacher>(teacher, await Context.Teachers.FindAsync(teacher.Id));
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            var teacher = Context.Teachers.First();

            var result = await _controller.DeleteAsync(teacher.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Teacher));
            Assert.AreEqual(teacher.Id, (model as Teacher)!.Id);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            var teacher = Context.Teachers.First();

            var result = await _controller.DeleteAsync(teacher);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(0, Context.Teachers.Count());
        }
    }
}
