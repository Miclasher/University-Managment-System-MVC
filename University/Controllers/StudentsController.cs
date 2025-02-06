using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.DataLayer;
using University.DataLayer.Models;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var Students = await _context.Students
                .Include(e => e.Group)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();

            ViewData["GroupsPresent"] = await _context.Groups.AnyAsync();

            if (TempData != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View(Students);
        }

        public async Task<IActionResult> CreateAsync()
        {
            await LoadViewBagAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Student student)
        {
            await LoadViewBagAsync();

            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var Student = await _context.Students.FindAsync(id);

            if (Student is null)
            {
                return RedirectToAction("Index");
            }

            await LoadViewBagAsync();

            return View(Student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Student student)
        {
            await LoadViewBagAsync();

            if (!await _context.Students.ContainsAsync(student))
            {
                TempData["ErrorMessage"] = "It looks like student was already deleted.";

                return RedirectToAction("Index");
            }

            if (!await _context.Groups.AnyAsync(e => e.Id == student.GroupId))
            {
                TempData["ErrorMessage"] = "It looks like group that you have selected has been already deleted.";

                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(student);
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var Student = await _context.Students.FindAsync(id);

            if (Student is null)
            {
                return RedirectToAction("Index");
            }

            await LoadViewBagAsync();

            return View(Student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Student student)
        {
            await LoadViewBagAsync();

            if (!await _context.Students.ContainsAsync(student))
            {
                TempData["ErrorMessage"] = "It looks like student was already deleted.";

                return RedirectToAction("Index");
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task LoadViewBagAsync()
        {
            ViewBag.Groups = await _context.Groups.OrderBy(e => e.Name).ToListAsync();
        }
    }
}
