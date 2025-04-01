using Microsoft.AspNetCore.Mvc;

namespace LoginRegister.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
