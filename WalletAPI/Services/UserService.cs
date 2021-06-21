using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.CustomException;
using WalletAPI.Loaders;
using WalletAPI.Models;

namespace WalletAPI.Services
{
    public class UserService
    {
        private readonly WalletContext _context;

        private readonly AccountService AccountService;

        public UserService(WalletContext context, IRateLoader rateLoader)
        {
            _context = context;
            AccountService = new AccountService(rateLoader);
        }

        public void CreateUser(string userName)
        {
            var user = new User
            {
                Name = userName,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<List<string>> GetUsers()
        {
            var users = await _context.Users.Select(x => x.Name).ToListAsync().ConfigureAwait(false);

            return users;
        }

        public async Task<IEnumerable<string>> GetAccountInfo(string userName)
        {
            var user = await _context.Users
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name.Contains(userName)).ConfigureAwait(false);
            var account = AccountService.CreateAccount(currency);

            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public async Task DepositMoney(string userName, string currency, decimal count)
        {
            var user = await _context.Users
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Name.Equals(userName))
                    .ConfigureAwait(false);

            if (user == null)
            {
                throw new UserWasNullException();
            }

            var account = user.Accounts.FirstOrDefault(x => x.Currency.Equals(currency));

            AccountService.AddMoney(account, count);

            _context.SaveChanges();
        }

        public async Task WithdrawMoney(string userName, string currency, decimal count)
        {
            var user = await _context.Users
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Name.Equals(userName))
                    .ConfigureAwait(false);

            if (user == null)
            {
                throw new UserWasNullException();
            }

            var account = user.Accounts.Where(x => x.Equals(currency)).FirstOrDefault();
            AccountService.WithdrawMoney(account, count);

            _context.SaveChanges();
        }

        public async Task TransferMoneyAsync(string userName, string currencyFrom, string currencyTo, decimal count)
        {
            var user = await _context.Users
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

            _context.SaveChanges();
        }
    }
}
