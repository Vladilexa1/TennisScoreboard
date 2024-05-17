using Microsoft.AspNetCore.Mvc;

namespace TennisScoreboard.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult StartView()
        {
            return View("StartView");
        }
    }
}
