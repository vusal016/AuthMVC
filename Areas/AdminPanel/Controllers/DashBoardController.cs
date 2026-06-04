using AuthAdminCrud.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAdminCrud.MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        readonly AuthDbContext _authDb;
        public DashBoardController(AuthDbContext authDb)
        {
            _authDb = authDb;
        }
        public IActionResult Index()
        {
            var products = _authDb.Products.Select(x => new ProductVM
            {
                Name = x.Name,
                Price = x.Price
            }).ToList();

            return View(products);
        }
    }
}
