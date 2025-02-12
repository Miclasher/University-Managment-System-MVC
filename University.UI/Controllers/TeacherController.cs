using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;
using University.UI.ViewModels;

namespace University.UI.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            var viewModel = new TeacherIndexViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                viewModel.ErrorMessage = errorMessage;
            }

            viewModel.Teachers = await _teacherService.GetAllAsync();

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(TeacherToCreateDTO teacher)
        {
            await _teacherService.CreateAsync(teacher);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Teacher = await _teacherService.GetByIdAsync(id);

            return View(Teacher.Adapt<TeacherToUpdateDTO>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(TeacherToUpdateDTO teacher)
        {
            await _teacherService.UpdateAsync(teacher);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Teacher = await _teacherService.GetByIdAsync(id);

            return View(Teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(TeacherDTO teacherToDelete)
        {
            await _teacherService.DeleteAsync(teacherToDelete.Id);

            return RedirectToAction("Index");
        }
    }
}
