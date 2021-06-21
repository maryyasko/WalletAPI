using System.Collections.Generic;
using System.Linq;
using WalletAPI.CustomException;
using WalletAPI.Loaders;
using WalletAPI.Models;

namespace WalletAPI.Services
{
    public class AccountService
    {
        private readonly List<CurrencyInformation> RatesInformation;

        public AccountService(IRateLoader rateLoader)
        {
            RatesInformation = rateLoader.GetRateInformation().ToList();
        }

        private void ValidateCurrency(string currancyName)
        {
            if (!RatesInformation.Any(x => x.Currency.Equals(currancyName)))
            {
                throw new RateException();
            }
        }

        private void CheckCurrency(Account account, decimal count)
        {
            if (account == null || account.Balance < count)
            {
                throw new CurrencyException();
            }
        }

        public void WithdrawMoney(Account account, decimal count)
        {
            CheckCurrency(account, count);

            account.Balance -= count;
        }

        public Account CreateAccount(string currancy)
        {
            ValidateCurrency(currancy);

            var currency = new Account
            {
                Balance = 0,
                Currency = currancy,
            };

            return currency;
        }

        public void AddMoney(Account account, decimal count)
        {
            if (account == null)
            {
                throw new AccountException();
            }

            account.Balance += count;
        }

        public void TransferMoney(Account accountFrom, Account accountTo, decimal count)
        {
            WithdrawMoney(accountFrom, count);

            if (accountTo == null)
            {
                throw new AccountException();
            }

            // Добавить перевод в другую валюту.
            AddMoney(accountTo, count);
        }
    }
}
