using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;
using University.UI.ViewModels;

namespace University.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            var viewModel = new CourseIndexViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                viewModel.ErrorMessage = errorMessage;
            }

            viewModel.Courses = await _courseService.GetAllAsync();

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CourseToCreateDTO course)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { errorMessage = "Invalid course data" });
            }

            await _courseService.CreateAsync(course);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var course = await _courseService.GetByIdAsync(id);

            return View(course.Adapt<CourseToUpdateDTO>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CourseToUpdateDTO course)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { errorMessage = "Invalid course data" });
            }

            await _courseService.UpdateAsync(course);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var course = await _courseService.GetByIdAsync(id);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(CourseDTO courseToDelete)
        {
            await _courseService.DeleteAsync(courseToDelete.Id);

            return RedirectToAction("Index");
        }
    }
}
