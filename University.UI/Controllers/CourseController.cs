using Microsoft.AspNetCore.Mvc;

namespace University.UI.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
