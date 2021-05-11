using Domain.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BankApiClient
    {
        private readonly HttpClient client;

        public BankApiClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<int> RegisterNewCustomerAsync(Customer customer)
        {
            var response = await client.PostAsJsonAsync("api/customer", customer);

            customer = await response.Content.ReadFromJsonAsync<Customer>();

            return customer.CustomerId;
        }
        public async Task AddAccountType(AccountType accountType)
        {
            await client.PostAsJsonAsync("api/AccountType", accountType);
        }
        public async Task<List<Account>> GetAccounts(int customerId)
        {
            return await client.GetFromJsonAsync<List<Account>>($"api/customer/Accounts/{customerId}");
        }
        public async Task<List<Transaction>> GetTransactionsAsync(int accountId)
        {
            return await client.GetFromJsonAsync<List<Transaction>>($"api/account/GetTransactions/{accountId}");
        }
        public void Transfer(int sum, int from, int to)
        {

        }
        public void Withdraw(int sum, int accountId)
        {

        }
        public void Deposit(int sum, int accountId)
        {

        }
        public void AddLoan(int sum, int accountId)
        {

        }
        public async Task<List<Customer>> GetCustomerPageAsync(string searchString, int page)
        {
            if(searchString.StartsWith('#'))
            {
                searchString = searchString.Remove(0, 1);
                return await client.GetFromJsonAsync<List<Customer>>($"api/Customer/{searchString}");
            }
            else
            {
                int offset = page * 10;

                if (page == 0)
                {
                    offset = 0;
                }

                return await client.GetFromJsonAsync<List<Customer>>($"api/Customer/?searchString={searchString}&offset={offset}");
            }
        }
        public async Task<Customer> GetCustomer(int id)
        {
            return await client.GetFromJsonAsync<Customer>($"api/Customer/{id}");
        }
        public async Task<Customer> GetAccount(int id)
        {
            return await client.GetFromJsonAsync<Customer>($"api/Account/{id}");
        }
        public async Task<List<AccountType>> GetAccountTypes()
        {
            return await client.GetFromJsonAsync<List<AccountType>>($"api/AccountType/all");
        }

    }
}
