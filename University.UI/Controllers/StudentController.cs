using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;

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
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            if (await _studentService.CanBeCreatedAsync())
            {
                ViewData["CanBeCreated"] = true;
            }

            var Students = await _studentService.GetAllAsync();

            return View(Students);
        }

        public async Task<IActionResult> CreateAsync()
        {
            await LoadViewBagAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(StudentToCreateDTO student)
        {
            await _studentService.CreateAsync(student);

            await LoadViewBagAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Student = await _studentService.GetByIdAsync(id);

            await LoadViewBagAsync();

            return View(Student.Adapt<StudentToUpdateDTO>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(StudentToUpdateDTO student)
        {
            await LoadViewBagAsync();

            await _studentService.UpdateAsync(student);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Student = await _studentService.GetByIdAsync(id);

            await LoadViewBagAsync();

            return View(Student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(StudentDTO studentToDelete)
        {
            await LoadViewBagAsync();

            await _studentService.DeleteAsync(studentToDelete.Id);

            return RedirectToAction("Index");
        }

        private async Task LoadViewBagAsync()
        {
            await _viewDataService.LoadViewDataForStudents(ViewData);

            ViewBag.Groups = ViewData["Groups"];
        }
    }
}
