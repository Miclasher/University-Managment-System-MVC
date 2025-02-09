using Microsoft.AspNetCore.Mvc;

namespace University.UI.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
