using AuthAdminCrud.MVC.Areas.Helper;
using AuthAdminCrud.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthAdminCrud.MVC.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AuthDbContext _authDb;

        public ProductController(AuthDbContext authDb)
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
        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductCreateVM productCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return View(productCreateVM);
            }
            var product = new Product(productCreateVM.Name, productCreateVM.Price, productCreateVM.ButtonText);
            var image = await FileHelper.SaveFileAsync(productCreateVM.Image);
            product.SetImageUrl(image);
            _authDb.Products.Add(product);
            await _authDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ProductEdit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductEditVM productEditVM)
        {
            if (!ModelState.IsValid)
            {
                return View(productEditVM);
            }
            var product = await _authDb.Products.FirstOrDefaultAsync(p => p.Id == productEditVM.Id);
            if (product == null)
            {
                return NotFound();
            }
            product.UpdateName(productEditVM.Name);
            product.UpdatePrice(productEditVM.Price);
            product.UpdateButtonText(productEditVM.ButtonText);
            if (productEditVM.Image != null)
            {
                var image = await FileHelper.SaveFileAsync(productEditVM.Image);
                product.SetImageUrl(image);
            }
            await _authDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(Guid id)
        {
            var product = await _authDb.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _authDb.Products.Remove(product);
                await _authDb.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
