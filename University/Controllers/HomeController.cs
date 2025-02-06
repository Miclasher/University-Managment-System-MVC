using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using University.DataLayer;
using University.Models;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private readonly UniversityContext _context;

        public HomeController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var courses = await _context.Courses.OrderBy(e => e.Name).ToListAsync();

            return View(courses);
        }

        public async Task<IActionResult> CourseGroupsAsync(Guid courseId)
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

        public async Task<IActionResult> GroupStudentsAsync(Guid groupId)
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
