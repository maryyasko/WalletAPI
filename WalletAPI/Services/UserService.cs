using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.CustomException;
using WalletAPI.Models;

namespace WalletAPI.Services
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Контекст для работы с БД.
        /// </summary>
        private readonly WalletContext Context;

        /// <summary>
        /// Сервис для работы с счетами.
        /// </summary>
        private readonly IAccountService AccountService;

        public UserService(WalletContext context, IAccountService accountService)
        {
            Context = context;
            AccountService = accountService;
        }

        /// <inheritdoc />
        public void CreateUser(string userName)
        {
            var user = new User
            {
                Name = userName,
            };

            Context.Users.Add(user);
            Context.SaveChanges();
        }

        /// <inheritdoc />
        public async Task<List<string>> GetUsers()
        {
            var users = await Context.Users.Select(x => x.Name).ToListAsync().ConfigureAwait(false);

            return users;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task CreateAccount(string userName, string currency)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Name.Contains(userName)).ConfigureAwait(false);
            var account = AccountService.CreateAccount(currency);

            Context.Accounts.Add(account);
            Context.SaveChanges();
        }

        /// <inheritdoc />
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

            AccountService.DepositMoney(account, count);

            Context.SaveChanges();
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task TransferMoney(string userName, string currencyFrom, string currencyTo, decimal count)
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
