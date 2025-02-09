using Microsoft.AspNetCore.Mvc;

namespace University.UI.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
