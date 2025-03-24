using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.UI.ViewModels;

namespace University.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ICourseService _courseService;

        public HomeController(IGroupService groupService, ICourseService courseService)
        {
            _groupService = groupService;
            _courseService = courseService;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            var viewModel = new HomeIndexViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                viewModel.ErrorMessage = errorMessage;
            }

            viewModel.Courses = await _courseService.GetAllAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> CourseGroupsAsync(Guid courseId)
        {
            var course = await _courseService.GetCourseWithGroupDetailsByIdAsync(courseId);

            return View(course.Groups);
        }

        public async Task<IActionResult> GroupStudentsAsync(Guid groupId)
        {
            var group = await _groupService.GetByIdAsync(groupId);

            var model = new GroupStudentsViewModel
            {
                CourseId = group.CourseId,
                Students = group.Students
            };

            return View(model);
        }
    }
}
