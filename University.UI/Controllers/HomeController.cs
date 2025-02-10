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

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            var courses = await _serviceManager.CourseService.GetAllAsync();

            return View(courses);
        }

        public async Task<IActionResult> CourseGroupsAsync(Guid courseId)
        {
            var course = await _serviceManager.CourseService.GetCourseWithGroupDetailsByIdAsync(courseId);

            TempData["CourseId"] = courseId;

            return View(course.Groups);
        }

        public async Task<IActionResult> GroupStudentsAsync(Guid groupId)
        {
            var group = await _serviceManager.GroupService.GetByIdAsync(groupId);

            if (TempData != null)
            {
                ViewBag.CourseId = TempData["CourseId"];
            }

            return View(group.Students);
        }
    }
}
