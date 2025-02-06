using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.DataLayer;
using University.DataLayer.Models;

namespace University.Controllers
{
    public class TeachersController : Controller
    {
        private readonly UniversityContext _context;

        public TeachersController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var Teachers = await _context.Teachers
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();

            if (TempData != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View(Teachers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var Teacher = await _context.Teachers.FindAsync(id);

            if (Teacher is null)
            {
                return RedirectToAction("Index");
            }

            return View(Teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Teacher teacher)
        {
            if (!_context.Teachers.Contains(teacher))
            {
                TempData["ErrorMessage"] = "It looks like teacher was already deleted.";

                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var Teacher = await _context.Teachers.FindAsync(id);

            if (Teacher is null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IsTutor = await _context.Groups.AnyAsync(e => e.TeacherId == id);

            return View(Teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Teacher teacher)
        {
            if (!_context.Teachers.Contains(teacher))
            {
                TempData["ErrorMessage"] = "It looks like teacher was already deleted.";

                return RedirectToAction("Index");
            }

            _context.Teachers.Remove(teacher);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
