using BankApp2.ViewModels;
using Data.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApp2.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                if (User.IsInRole("Customer"))
                    return RedirectToAction("Index", "Customer");
                else if (User.IsInRole("Admin"))
                    return RedirectToAction("Customers", "Admin");
            }
            ModelState.AddModelError(string.Empty, "Invalid login");
            return View();
        }
    }
}
