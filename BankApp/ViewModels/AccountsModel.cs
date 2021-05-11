using System.Collections.Generic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankApp.ViewModels
{
    public class AccountsModel
    {
        public List<Account> Accounts { get; set; }
        public SelectList AccountTypes { get; set; }
    }
}
