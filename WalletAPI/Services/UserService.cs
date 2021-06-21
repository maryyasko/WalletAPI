using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.CustomException;
using WalletAPI.Loaders;
using WalletAPI.Models;

namespace WalletAPI.Services
{
    public class UserService : IUserService
    {
        private readonly WalletContext Context;

        private readonly IAccountService AccountService;

        public UserService(WalletContext context, IAccountService accountService)
        {
            Context = context;
            AccountService = accountService;
        }

        public void CreateUser(string userName)
        {
            var user = new User
            {
                Name = userName,
            };

            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public async Task<List<string>> GetUsers()
        {
            var users = await Context.Users.Select(x => x.Name).ToListAsync().ConfigureAwait(false);

            return users;
        }

        public async Task<IEnumerable<string>> GetAccountInfo(string userName)
        {
            var user = await Context.Users
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Name.Equals(userName))
                    .ConfigureAwait(false);

            if (user == null)
            {
                throw new UserWasNullException();
            }

            var accountInfo = user.GetAccountInfo();

            return accountInfo;
        }

        public async Task CreateAccount(string userName, string currency)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Name.Contains(userName)).ConfigureAwait(false);
            var account = AccountService.CreateAccount(currency);

            Context.Accounts.Add(account);
            Context.SaveChanges();
        }

        public async Task DepositMoney(string userName, string currency, decimal count)
        {
            var user = await Context.Users
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Name.Equals(userName))
                    .ConfigureAwait(false);

            if (user == null)
            {
                throw new UserWasNullException();
            }

            var account = user.Accounts.FirstOrDefault(x => x.Currency.Equals(currency));

            AccountService.AddMoney(account, count);

            Context.SaveChanges();
        }

        public async Task WithdrawMoney(string userName, string currency, decimal count)
        {
            var user = await Context.Users
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Name.Equals(userName))
                    .ConfigureAwait(false);

            if (user == null)
            {
                throw new UserWasNullException();
            }

            var account = user.Accounts.Where(x => x.Equals(currency)).FirstOrDefault();
            AccountService.WithdrawMoney(account, count);

            Context.SaveChanges();
        }

        public async Task TransferMoneyAsync(string userName, string currencyFrom, string currencyTo, decimal count)
        {
            var user = await Context.Users
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Name.Equals(userName))
                    .ConfigureAwait(false);

            if (user == null)
            {
                throw new UserWasNullException();
            }

            var accountFrom = user.Accounts.Where(x => x.Currency.Equals(currencyFrom)).FirstOrDefault();
            var accountTo = user.Accounts.Where(x => x.Currency.Equals(currencyTo)).FirstOrDefault();

            AccountService.TransferMoney(accountFrom, accountTo, count);

            Context.SaveChanges();
        }
    }
}
