using Microsoft.AspNetCore.Mvc;
using MrCoconut.WebApp.Infra.Repositories;
using MrCoconut.WebApp.Infra.Repositories.Interfaces;
using MrCoconut.WebApp.Models;

namespace MrCoconut.WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser(IFormCollection form)
        {
            var user = new User(form["name"], form["email"], form["password"]);

            if (form["password"] != form["confirm"])
            {
                return LocalRedirect("~/Register");
            }

            if (!user.IsValid)
            {
                ViewData["ErrorMessage"] = user.Notifications;
                return View("Index");
            }

            userRepository.CreateUser(user);
            return LocalRedirect("~/Login");
        }
    }
}
