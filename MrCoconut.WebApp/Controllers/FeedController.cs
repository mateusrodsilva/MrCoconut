using Microsoft.AspNetCore.Mvc;

namespace MrCoconut.WebApp.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
