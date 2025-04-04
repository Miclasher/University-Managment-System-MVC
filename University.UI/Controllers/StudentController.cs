﻿using Mapster;
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

            viewModel.CanBeCreated = await _studentService.CanBeCreatedAsync();

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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { errorMessage = "Invalid student data" });
            }

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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { errorMessage = "Invalid student data" });
            }

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
