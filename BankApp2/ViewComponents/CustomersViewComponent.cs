using Core.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp2.ViewComponents
{
    public class CustomersViewComponent : ViewComponent
    {
        private readonly BankApiClient bankApiClient;

        public CustomersViewComponent(BankApiClient bankApiClient)
        {
            this.bankApiClient = bankApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync(int page)
        {
            ViewBag.Page = page;
            List<Customer> customers = await bankApiClient.GetCustomerPageAsync(page);
            return View(customers);
        }
    }
}
