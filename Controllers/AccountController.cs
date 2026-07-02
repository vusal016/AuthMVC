
using AuthAdminCrud.MVC.Areas.Helper;
using AuthAdminCrud.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AuthAdminCrud.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)

        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var profile = new ProfileVM
            {
                FullName = user.FullName,
                Email = user.Email,
                ImageUrl = user.ImageUrl
            };

            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileVM profileVm)
        {
            if (!ModelState.IsValid)
            {
                return View(profileVm);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            user.FullName = profileVm.FullName;
            user.Email = profileVm.Email;
            user.ImageUrl = profileVm.File != null ? await FileHelper.SaveFileAsync(profileVm.File) : user.ImageUrl;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(profileVm);
            }
            return RedirectToAction("Profile");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("Profile");
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            //Eger UserName Uniqe edib yene de FullName ile axtaracaqsansa,FirstOrDefault lazimdir.
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.FullName == loginVm.UserName)
            ?? await _userManager.FindByEmailAsync(loginVm.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginVm);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginVm.Password, true, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginVm);
            }
            //Admin Paneline daxil olduqda admin paneline, adi user daxil olduqda home page-e gonderirik
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index", "DashBoard", new { area = "AdminPanel" });
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            var user = new AppUser
            {
                //UserName = registerVm.Email---Bu forma daha yaxsidir cunki email unikal olmalidir
                UserName = registerVm.Email,
                FullName = registerVm.FullName,
                Email = registerVm.Email,
            };
            var result = await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVm);
            }
            await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}