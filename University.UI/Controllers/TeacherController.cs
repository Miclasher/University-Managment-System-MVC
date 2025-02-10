using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;

namespace University.UI.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public TeacherController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            var Teachers = await _serviceManager.TeacherService.GetAllAsync();

            return View(Teachers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TeacherToCreateDTO teacher)
        {
            await _serviceManager.TeacherService.CreateAsync(teacher);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Teacher = await _serviceManager.TeacherService.GetByIdAsync(id);

            return View(Teacher.Adapt<TeacherToUpdateDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(TeacherToUpdateDTO teacher)
        {
            await _serviceManager.TeacherService.UpdateAsync(teacher);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Teacher = await _serviceManager.TeacherService.GetByIdAsync(id);

            return View(Teacher);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(TeacherDTO teacherToDelete)
        {
            await _serviceManager.TeacherService.DeleteAsync(teacherToDelete.Id);

            return RedirectToAction("Index");
        }
    }
}
