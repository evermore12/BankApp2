using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.ViewModels;
using Core.Services;
using Data.EFModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BankApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly BankApiClient bankApiClient;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(BankApiClient bankApiClient, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.bankApiClient = bankApiClient;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> EditCustomer(int id)
        {
            Customer customer = await bankApiClient.GetCustomer(id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult EditCustomer(Customer customer, LoginModel loginModel, AccountsModel accountsModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error123");
                //return PartialView("Customer", customer);
                List<ModelError> errors = new();
                foreach (var item in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in item.Errors)
                    {
                        errors.Add(error);
                    }
                }
                return PartialView("_MessageSuccess", "Customer added");
            }
            return Ok();

        }

        public async Task<IActionResult> AddCustomer()
        {
            return View();
        }
        public async Task<IActionResult> AddCustomer(Customer customer, LoginModel loginModel, AccountsModel accountsModel)
        {
            return View();
        }
       
        public async Task<IActionResult> AddLoginAsync(LoginModel model, int customerId)
        {
            ApplicationUser user = new ApplicationUser { UserName = model.Username, CustomerId = customerId };
            var result = await userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");
                return ViewComponent("IdentityForm", customerId);
            }
            return ViewComponent("IdentityForm", customerId);

        }
        public async Task<IActionResult> RemoveLoginAsync(int customerId)
        {
            ApplicationUser user = userManager.Users.SingleOrDefault(x => x.CustomerId == customerId);

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return ViewComponent("IdentityForm", customerId);
            }
            return ViewComponent("IdentityForm", customerId);

        }
    }
}
