using Microsoft.AspNetCore.Mvc;

namespace University.UI.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
