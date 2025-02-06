using Microsoft.AspNetCore.Mvc;
using University.Controllers;
using University.DataLayer.Models;

namespace University.Tests
{
    [TestClass]
    public class GroupsControllerTests : CrudControllerTestSetup
    {
        private GroupsController _controller = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _controller = new GroupsController(Context);
        }

        [TestMethod]
        public async Task IndexAsyncTest()
        {
            var result = await _controller.IndexAsync();
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(IEnumerable<Group>));
            Assert.AreEqual(1, (model as IEnumerable<Group>)!.Count());
        }

        [TestMethod]
        public async Task CreateAsyncTest()
        {
            var newGroup = new Group { Id = Guid.NewGuid(), Name = "New Group", CourseId = Context.Courses.First().Id, TeacherId = Context.Teachers.First().Id };

            var result = await _controller.CreateAsync(newGroup);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(2, Context.Groups.Count());
        }

        [TestMethod]
        public async Task EditAsyncTest()
        {
            var group = Context.Groups.First();

            var result = await _controller.EditAsync(group.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Group));
            Assert.AreEqual(group.Id, (model as Group)!.Id);
        }

        [TestMethod]
        public async Task EditAsyncTest2()
        {
            var group = Context.Groups.First();

            group.Name = "Edited";

            await _controller.EditAsync(group);

            Assert.AreEqual<Group>(group, await Context.Groups.FindAsync(group.Id));
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            var group = Context.Groups.First();

            var result = await _controller.DeleteAsync(group.Id);
            var model = (result as ViewResult)!.Model;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Group));
            Assert.AreEqual(group.Id, (model as Group)!.Id);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            var group = Context.Groups.First();

            var result = await _controller.DeleteAsync(group);
            var redirectToActionResult = result as RedirectToActionResult;

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectToActionResult!.ActionName);
            Assert.AreEqual(0, Context.Groups.Count());
        }
    }
}
