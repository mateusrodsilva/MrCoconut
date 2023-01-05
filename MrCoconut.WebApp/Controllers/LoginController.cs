using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MrCoconut.WebApp.Infra.Repositories.Interfaces;
using MrCoconut.WebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MrCoconut.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository userRepository;

        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Login(IFormCollection form)
        {
            var loged = userRepository.Login(form["email"], form["password"]);

            if (loged.Result.Erros.Any())
            {
                ViewData["Message"] = $"Wrong data, {loged.Result.Erros.FirstOrDefault()}";
                return View("Index");
            }

            if (loged.Result != null)
            {
                ViewBag.LoggedIn = loged.Result.Id;
            }
            else
            {
                ViewData["Message"] = "Wrong data, try again...";
                return View("Index");
            }

            return LocalRedirect("~/Feed/Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
