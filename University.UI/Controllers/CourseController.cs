using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;

namespace University.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public CourseController(IServiceManager serviceManager)
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CourseToCreateDTO course)
        {
            await _serviceManager.CourseService.CreateAsync(course);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var course = await _serviceManager.CourseService.GetByIdAsync(id);

            return View(course.Adapt<CourseToUpdateDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(CourseToUpdateDTO course)
        {
            await _serviceManager.CourseService.UpdateAsync(course);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var course = await _serviceManager.CourseService.GetByIdAsync(id);
            
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(CourseDTO courseToDelete)
        {
            await _serviceManager.CourseService.DeleteAsync(courseToDelete.Id);

            return RedirectToAction("Index");
        }
    }
}
