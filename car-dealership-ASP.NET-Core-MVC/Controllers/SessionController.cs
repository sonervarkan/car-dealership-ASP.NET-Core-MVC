using car_dealership_ASP.NET_Core_MVC.Data;
using car_dealership_ASP.NET_Core_MVC.Models;
using car_dealership_ASP.NET_Core_MVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity;

namespace car_dealership_ASP.NET_Core_MVC.Controllers
{
    public class SessionController : Controller
    {
        private readonly CarDealershipDbContext _context;

        public SessionController(CarDealershipDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                Roles = _context.Roles.ToList(),
                UserVMAdd = new Users()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var passwordHasher = new PasswordHasher<Users>();
                    model.UserVMAdd.Password = passwordHasher.HashPassword(model.UserVMAdd, model.UserVMAdd.Password);
                    
                    _context.Users.Add(model.UserVMAdd);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                   
                    ViewBag.ErrorMessage = "An error occurred during registration: " + ex.Message;
                   
                    ModelState.AddModelError("", "An error occurred during registration. Please check your information.");
                }
            }

          
            model.Roles = _context.Roles.ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View();
            }

            var passwordHasher = new PasswordHasher<Users>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View();
            }

            // Giriş başarılı
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
