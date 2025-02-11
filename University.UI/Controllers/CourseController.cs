using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;

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
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            var courses = await _courseService.GetAllAsync();

            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CourseToCreateDTO course)
        {
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
