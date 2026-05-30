using System.Diagnostics;
using AuthAdminCrud.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthAdminCrud.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthDbContext _authDb;
        public HomeController(AuthDbContext authDb)
        {
            _authDb = authDb;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _authDb.Products.Select(p => new ProductVM
            {
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                ButtonText = p.ButtonText
            }).ToListAsync();
            return View(products);
        }
     
    }
}
