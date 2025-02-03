using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.DataLayer;
using University.Models;

namespace University.Controllers
{
    public class TeachersController : Controller
    {
        private readonly UniversityContext _context;

        public TeachersController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Teachers = await _context.Teachers.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync();

            return View(Teachers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Teacher Teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(Teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Teacher);
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
        public async Task<IActionResult> EditAsync(Teacher Teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Update(Teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Teacher);
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

            return View(Teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Teacher Teacher)
        {
            if (!_context.Teachers.Contains(Teacher))
            {
                return RedirectToAction("Index");
            }
            _context.Teachers.Remove(Teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
