using Microsoft.AspNetCore.Mvc;

namespace TesteTecnicoFIAP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            return View();
        }
    }
}
