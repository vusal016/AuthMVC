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
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                ButtonText = x.ButtonText
            }).ToList();

            return View(products);
        }
    }
}
