using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;

namespace University.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public StudentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            if (await _serviceManager.StudentService.CanBeCreatedAsync())
            {
                ViewData["CanBeCreated"] = true;
            }

            var Students = await _serviceManager.StudentService.GetAllAsync();

            return View(Students);
        }

        public async Task<IActionResult> CreateAsync()
        {
            await LoadViewBagAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentToCreateDTO student)
        {
            await _serviceManager.StudentService.CreateAsync(student);

            await LoadViewBagAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Student = await _serviceManager.StudentService.GetByIdAsync(id);

            await LoadViewBagAsync();

            return View(Student.Adapt<StudentToUpdateDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(StudentToUpdateDTO student)
        {
            await LoadViewBagAsync();

            await _serviceManager.StudentService.UpdateAsync(student);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Student = await _serviceManager.StudentService.GetByIdAsync(id);

            await LoadViewBagAsync();

            return View(Student);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(StudentDTO studentToDelete)
        {
            await LoadViewBagAsync();

            await _serviceManager.StudentService.DeleteAsync(studentToDelete.Id);

            return RedirectToAction("Index");
        }

        private async Task LoadViewBagAsync()
        {
            await _serviceManager.ViewDataService.LoadViewDataForStudents(ViewData);

            ViewBag.Groups = ViewData["Groups"];
        }
    }
}
