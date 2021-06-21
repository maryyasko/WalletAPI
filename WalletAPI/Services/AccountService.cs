using System.Collections.Generic;
using System.Linq;
using WalletAPI.CustomException;
using WalletAPI.Loaders;
using WalletAPI.Models;

namespace WalletAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<CurrencyInformation> CurrenciesInformation;

        public AccountService(IRateLoader rateLoader)
        {
            CurrenciesInformation = rateLoader.GetRateInformation().ToList();
        }

        private void ValidateCurrency(string currancyName)
        {
            if (!CurrenciesInformation.Any(x => x.Currency.Equals(currancyName)))
            {
                throw new CurrencyException();
            }
        }

        private void CheckCurrency(Account account, decimal count)
        {
            if (account == null || account.Balance < count)
            {
                throw new BalanceException();
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

            var account = new Account
            {
                Balance = 0,
                Currency = currancy,
            };

            return account;
        }

        public void DepositMoney(Account account, decimal count)
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

            var rateFrom = CurrenciesInformation.FirstOrDefault(x => x.Currency.Equals(accountFrom.Currency)).Rate;
            var rateTo = CurrenciesInformation.FirstOrDefault(x => x.Currency.Equals(accountTo.Currency)).Rate;

            count = count / rateFrom * rateTo;

            // Добавить перевод в другую валюту.
            DepositMoney(accountTo, count);
        }
    }
}
