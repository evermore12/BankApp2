using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp.ViewModels;
using Core.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankApp.ViewComponents
{
    public class CustomerAccountsViewComponent : ViewComponent
    {
        private readonly BankApiClient bankApiClient;

        public CustomerAccountsViewComponent(BankApiClient bankApiClient)
        {
            this.bankApiClient = bankApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            List<Account> accounts = await bankApiClient.GetAccounts(customerId);
            List<AccountType> accountTypes = await bankApiClient.GetAccountTypes();
            AccountsModel model = new AccountsModel
            {
                AccountTypes = new SelectList(accountTypes, "AccountTypeId", "TypeName"),
                Accounts = accounts
            };

            return View(model);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<AccountType> accountTypes = await bankApiClient.GetAccountTypes();
            AccountsModel model = new AccountsModel
            {
                AccountTypes = new SelectList(accountTypes, "AccountTypeId", "TypeName"),
            };

            return View(model);
        }
    }
}
