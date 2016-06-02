using Microsoft.AspNetCore.Mvc;

namespace RedisCacheWebPoc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
