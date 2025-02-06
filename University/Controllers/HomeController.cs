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

            var course = await _context.Courses
                .Where(e => e.Id == courseId)
                .Include(e => e.Groups)
                .ThenInclude(e => e.Students)
                .Include(e => e.Groups)
                .ThenInclude(e => e.Teacher)
                .FirstAsync();

            if (course is null)
            {
                return RedirectToAction("Index");
            }

            TempData["CourseId"] = courseId;

            return View(course.Groups.OrderBy(e => e.Name));
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

            if (TempData != null)
            {
                ViewBag.CourseId = TempData["CourseId"];
            }

            return View(students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
