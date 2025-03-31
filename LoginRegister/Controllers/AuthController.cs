using System.Diagnostics;
using LoginRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegister.Controllers
{
    public class AuthController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Save user to database
                // Redirect to login page
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Check if user exists in database
                // If user exists, redirect to dashboard
                // If user does not exist, return error message
            }
            return View(user);
        }
    }
}
