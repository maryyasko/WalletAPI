using System.Linq;
using WalletAPI.Models;

namespace WalletAPI
{
    public static class DBInitializer
    {
        public static void Initialize(WalletContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var account = new Account
            {
                AccountId = 1,
                Balance = 100,
                Currency = "USD"
            };

            var user = new User
            {
                UserId = 1,
                Name = "Mary",
            };

            user.Accounts.Add(account);

            context.Accounts.Add(account);
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
