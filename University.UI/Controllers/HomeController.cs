using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;

namespace University.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public HomeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var courses = await _serviceManager.CourseService.GetAllAsync();

            return View(courses);
        }
    }
}
