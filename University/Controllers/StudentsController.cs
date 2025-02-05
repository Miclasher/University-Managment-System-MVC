using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.DataLayer;
using University.Models;

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
            var Students = await _context.Students.Include(e => e.Group).OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync();

            return View(Students);
        }

        public async Task<IActionResult> CreateAsync()
        {
            await LoadViewBagAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Student Student)
        {
            await LoadViewBagAsync();

            if (ModelState.IsValid)
            {
                _context.Students.Add(Student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Student);
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
        public async Task<IActionResult> EditAsync(Student Student)
        {
            await LoadViewBagAsync();

            if (ModelState.IsValid)
            {
                _context.Students.Update(Student);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(Student);
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
        public async Task<IActionResult> DeleteAsync(Student Student)
        {
            await LoadViewBagAsync();

            if (!_context.Students.Contains(Student))
            {
                return RedirectToAction("Index");
            }

            _context.Students.Remove(Student);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task LoadViewBagAsync()
        {
            ViewBag.Groups = await _context.Groups.OrderBy(e => e.Name).ToListAsync();
        }
    }
}
