using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.DataLayer;
using University.Models;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UniversityContext _context;

        public CoursesController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.OrderBy(e => e.Name).ToListAsync();

            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var course = await _context.Courses.FindAsync(id);

            if (course is null)
            {
                return RedirectToAction("Index");
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var course = await _context.Courses.FindAsync(id);

            if (course is null)
            {
                return RedirectToAction("Index");
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Course course)
        {
            if (!_context.Courses.Contains(course))
            {
                return RedirectToAction("Index");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
