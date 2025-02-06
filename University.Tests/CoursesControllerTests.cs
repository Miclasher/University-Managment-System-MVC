using Microsoft.AspNetCore.Mvc;
using University.Controllers;
using University.DataLayer.Models;

namespace University.Tests
{
    [TestClass]
    public class CoursesControllerTests : CrudControllerTestSetup
    {
        private CoursesController _controller = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _controller = new CoursesController(Context);
        }

        [TestMethod]
        public async Task IndexAsyncTest()
        {
            var result = await _controller.IndexAsync();
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(IEnumerable<Course>));
            Assert.AreEqual(1, (model as IEnumerable<Course>)!.Count());
        }

        [TestMethod]
        public async Task CreateAsyncTest()
        {
            var newCourse = new Course { Id = Guid.NewGuid(), Name = "New Course", Description = "Course Description" };

            var result = await _controller.CreateAsync(newCourse);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(2, Context.Courses.Count());
        }

        [TestMethod]
        public async Task EditAsyncTest()
        {
            var course = Context.Courses.First();

            var result = await _controller.EditAsync(course.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Course));
            Assert.AreEqual(course.Id, (model as Course)!.Id);
        }

        [TestMethod]
        public async Task EditAsyncTest2()
        {
            var course = Context.Courses.First();

            course.Description = "Edited";

            await _controller.EditAsync(course);

            Assert.AreEqual<Course>(course, await Context.Courses.FindAsync(course.Id));
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            var course = Context.Courses.First();

            var result = await _controller.DeleteAsync(course.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Course));
            Assert.AreEqual(course.Id, (model as Course)!.Id);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            var course = Context.Courses.First();

            var result = await _controller.DeleteAsync(course);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(0, Context.Courses.Count());
        }
    }
}
