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
        //private readonly IHttpContextAccessor contextAccessor;
        private IUserRepository userRepository;

        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Login(IFormCollection form)
        {
            var loged = userRepository.Login(form["email"], form["password"]); 
            if (loged.Result != null)
            {
                ViewBag.LoggedIn = loged.Result.Id;
            }
            else
            {
                ViewData["Message"] = "Wrong data, try again...";
                return LocalRedirect("~/Login");
            }

            return LocalRedirect("~/Login");
        }

        public IActionResult Index()
        {
            return View();
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mrcoconut-authentication"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = Array.Empty<Claim>();

            claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.UserType.ToString()),
                new Claim("role", userInfo.UserType.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
            };

            // We configure our token and lifetime
            var token = new JwtSecurityToken
                (
                    "ergonomiks",
                    "ergonomiks",
                    claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
