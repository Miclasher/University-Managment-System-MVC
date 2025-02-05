using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.DataLayer;
using University.Models;

namespace University.Controllers
{
    public class GroupsController : Controller
    {
        private readonly UniversityContext _context;

        public GroupsController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var Groups = await _context.Groups
                .Include(e => e.Teacher)
                .Include(e => e.Students)
                .OrderBy(e => e.Name)
                .ToListAsync();

            return View(Groups);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewData["Teachers"] = await _context.Teachers.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync();
            ViewData["Courses"] = await _context.Courses.OrderBy(e => e.Name).ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Group Group)
        {
            if (ModelState.IsValid)
            {
                _context.Groups.Add(Group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Group);
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var Group = await _context.Groups.FindAsync(id);

            if (Group is null)
            {
                return RedirectToAction("Index");
            }

            return View(Group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Group Group)
        {
            if (ModelState.IsValid)
            {
                _context.Groups.Update(Group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Group);
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var Group = await _context.Groups.FindAsync(id);

            if (Group is null)
            {
                return RedirectToAction("Index");
            }

            return View(Group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Group Group)
        {
            if (!_context.Groups.Contains(Group))
            {
                return RedirectToAction("Index");
            }
            _context.Groups.Remove(Group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
