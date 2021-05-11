using System.Linq;
using BankApp.ViewModels;
using Core.Services;
using Data.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.ViewComponents
{
    public class CustomerLoginViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CustomerLoginViewComponent(BankApiClient bankApiClient, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public IViewComponentResult Invoke(int id)
        {
            ApplicationUser user = userManager.Users.SingleOrDefault(x => x.CustomerId == id);
            ViewBag.CustomerId = id;

            if (user == null)
            {
                return View("AddIdentity", new LoginModel());
            }
            else
            {
                return View("RemoveIdentity");
            }
        }
        
        public IViewComponentResult Invoke()
        {
            return View("AddIdentity", new LoginModel());
        }
    }
}
