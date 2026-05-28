using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

namespace AuthAdminCrud.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
