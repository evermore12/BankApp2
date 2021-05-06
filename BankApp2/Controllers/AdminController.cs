using Core.Services;
using Data.EFModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly BankApiClient bankApiService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(BankApiClient bankApiService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.bankApiService = bankApiService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Customers(int page)
        {
            return ViewComponent("Customers");
        }
    }
}
