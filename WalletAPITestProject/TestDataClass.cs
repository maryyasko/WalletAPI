using System.Collections.Generic;
using WalletAPI.Models;

namespace WalletAPITestProject
{
    public static class TestDataClass
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

        public static Account CreateAccount()
        {
            return new Account
            {
                AccountId = 2,
                Balance = 1000,
                Currency = "USD"
            };
        }

        public static Account CreateAccountTo()
        {
            return new Account
            {
                AccountId = 2,
                Balance = 1000,
                Currency = "RUB"
            };
        }

        public static List<CurrencyInformation> GetCurrencyInformation()
        {
            return new List<CurrencyInformation>
            {
                new CurrencyInformation
                {
                    Currency = "USD",
                    Rate = (decimal)2
                },
                new CurrencyInformation
                {
                    Currency = "RUB",
                    Rate = (decimal)87
                },
                new CurrencyInformation
                {
                    Currency = "ZAR",
                    Rate = (decimal)17
                },
            };
        }
    }
}
