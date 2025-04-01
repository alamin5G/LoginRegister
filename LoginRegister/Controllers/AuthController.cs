using System.Diagnostics;
using LoginRegister.Data;
using LoginRegister.DTO;
using LoginRegister.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginRegister.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("/auth/register-admin")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }


        [HttpPost]
        [Route("/auth/register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // check email already exists
            var emailExists = await _userManager.FindByEmailAsync(model.Email);

            if (emailExists != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            //check phone number already exists
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (phoneExists != null)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists");
                return View(model);
            }


            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = "Admin", // Force admin role
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "Admin");

                // Add related role-specific entity
                if (model.Role == "Admin")
                {
                    var admin = new Admin
                    {
                        UserID = user.Id

                    };

                    _context.Admins.Add(admin);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }


        public IActionResult Index()
        {
            return View();
        }


        // Register methods
        public IActionResult Register()
        {
            return View();
        }


        // Register methods (cont.)
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            // check email already exists
            var emailExists = await _userManager.FindByEmailAsync(model.Email);

            if (emailExists != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            //check phone number already exists
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (phoneExists != null) {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists");
                return View(model);
            }

            // create a new user object
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
                ProfilePicture = string.Empty, // Optional default
                DateOfBirth = DateTime.MinValue, // Or get it from model if added
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber
            };



            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                // Add related role-specific entity
                if (model.Role == "JobSeeker")
                {
                    var jobSeeker = new JobSeeker
                    {
                        UserID = user.Id,
                        Resume = "NotProvided.pdf", // default or ask in form
                        CoverLetter = "NotProvided.pdf",
                        PreferredJobTitle = "Unknown",
                        Location = "Unknown"

                    };

                    _context.JobSeekers.Add(jobSeeker);
                }
                else if (model.Role == "Employer")
                {
                    var employer = new Employer
                    {
                        UserID = user.Id,
                        CompanyName = "Unknown", // default or ask in form
                        CompanyDescription = "TBD",
                        Logo = ""
                    };

                    _context.Employers.Add(employer);
                }

                await _context.SaveChangesAsync(); // Save the related entity

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }




        // Login methods
        public IActionResult Login()
        {
            return View();
        }


        // Login methods (cont.)
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
                

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
            {
                ModelState.AddModelError("", "Invalid email or password");
                ViewBag.Message = "Invalid credentials, please try again.";
                return View(model);
            }

            return user.Role switch
            {
                "Admin" => RedirectToAction("Dashboard", "Admin"),
                "JobSeeker" => RedirectToAction("Dashboard", "JobSeeker"),
                "Employer" => RedirectToAction("Dashboard", "Employer"),
                _ => RedirectToAction("Login")
            };
        }
    }
}
