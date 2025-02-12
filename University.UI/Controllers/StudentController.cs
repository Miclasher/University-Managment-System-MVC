using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;
using University.UI.ViewModels;

namespace University.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IViewDataService _viewDataService;

        public StudentController(IStudentService studentService, IViewDataService viewDataService)
        {
            _studentService = studentService;
            _viewDataService = viewDataService;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            var viewModel = new StudentIndexViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                viewModel.ErrorMessage = errorMessage;
            }

            if (await _studentService.CanBeCreatedAsync())
            {
                viewModel.CanBeCreated = true;
            }
            else
            {
                viewModel.CanBeCreated = false;
            }

            viewModel.Students = await _studentService.GetAllAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var viewModel = new StudentCreateViewModel();

            viewModel.Groups = await _viewDataService.LoadViewDataForStudents();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(StudentToCreateDTO student)
        {
            await _studentService.CreateAsync(student);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var viewModel = new StudentEditViewModel();

            viewModel.Student = (await _studentService.GetByIdAsync(id)).Adapt<StudentToUpdateDTO>();

            viewModel.Groups = await _viewDataService.LoadViewDataForStudents();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(StudentToUpdateDTO student)
        {
            await _studentService.UpdateAsync(student);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var viewModel = new StudentDeleteViewModel();

            viewModel.Student = await _studentService.GetByIdAsync(id);

            viewModel.Groups = await _viewDataService.LoadViewDataForStudents();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(StudentDTO studentToDelete)
        {
            await _studentService.DeleteAsync(studentToDelete.Id);

            return RedirectToAction("Index");
        }
    }
}
