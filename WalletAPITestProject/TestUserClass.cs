using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletAPI.Models;

namespace WalletAPITestProject
{
    public static class TestUserClass
    {
        public static User CreateTestUser()
        {
            var accounts = new List<Account>
            {
                new Account
                {
                    Currency = "USD",
                    Balance = 100
                },

                new Account
                {
                    Currency = "TRY",
                    Balance = 100
                }
            };

            var user = new User
            {
                UserId = 1,
                Name = "TestUser",
            };

            user.Accounts.AddRange(accounts);

            return user;
        }
    }
}
