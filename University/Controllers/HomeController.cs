using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private readonly UniversityContext _context;

        public HomeController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.OrderBy(e => e.Name).ToListAsync();

            return View(courses);
        }

        public async Task<IActionResult> CourseGroups(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var groups = await _context.Groups
                .Include(e => e.Teacher)
                .Include(e => e.Students)
                .Where(e => e.CourseId == courseId)
                .OrderBy(e => e.Name)
                .ToListAsync();

            return View(groups);
        }

        public async Task<IActionResult> GroupStudents(Guid groupId)
        {
            if (groupId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var students = await _context.Students
                .Where(e => e.GroupId == groupId)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();

            return View(students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
