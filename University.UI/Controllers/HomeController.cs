using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using University.Services.Abstractions;

namespace University.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public HomeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var courses = await _serviceManager.CourseService.GetAllAsync();

            return View(courses);
        }

        public async Task<IActionResult> CourseGroups(Guid courseId)
        {
            var course = await _serviceManager.CourseService.GetCourseWithGroupDetailsByIdAsync(courseId);

            if (course == null)
            {
                return RedirectToAction("IndexAsync");
            }

            TempData["CourseId"] = courseId;

            return View(course.Groups);
        }

        public async Task<IActionResult> GroupStudents(Guid groupId)
        {
            var group = await _serviceManager.GroupService.GetByIdAsync(groupId);

            if (group == null)
            {
                return RedirectToAction("IndexAsync");
            }

            if (TempData != null)
            {
                ViewBag.CourseId = TempData["CourseId"];
            }

            return View(group.Students);
        }
    }
}
