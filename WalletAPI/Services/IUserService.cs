using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.Services
{
    public interface IUserService
    {
        void CreateUser(string userName);

        Task<List<string>> GetUsers();

        Task<IEnumerable<string>> GetAccountInfo(string userName);

        Task CreateAccount(string userName, string currency);

        Task DepositMoney(string userName, string currency, decimal count);

        Task WithdrawMoney(string userName, string currency, decimal count);

        Task TransferMoneyAsync(string userName, string currencyFrom, string currencyTo, decimal count);
    }
}
