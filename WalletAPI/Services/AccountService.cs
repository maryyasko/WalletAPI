using System.Collections.Generic;
using System.Linq;
using WalletAPI.CustomException;
using WalletAPI.Loaders;
using WalletAPI.Models;

namespace WalletAPI.Services
{
    /// <summary>
    /// Сервис для работы с счетами пользователя.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Информация о текущем курсе валют.
        /// </summary>
        private readonly List<CurrencyInformation> CurrenciesInformation;

        public AccountService(IRateLoader rateLoader)
        {
            CurrenciesInformation = rateLoader.GetRateInformation().ToList();
        }

        /// <summary>
        /// Проверить, что текущая валюта есть в списке валют.
        /// </summary>
        /// <param name="currancy">Название валюты.</param>
        private void ValidateCurrency(string currancy)
        {
            if (!CurrenciesInformation.Any(x => x.Currency.Equals(currancy)))
            {
                throw new CurrencyException();
            }
        }

        /// <summary>
        /// Проверить, что с текущего счета можно снять заданную сумму.
        /// </summary>
        /// <param name="account">Счет.</param>
        /// <param name="count">Сумма для снятия.</param>
        private void CheckCurrency(Account account, decimal count)
        {
            if (account == null || account.Balance < count)
            {
                throw new BalanceException();
            }
        }

        /// <inheritdoc />
        public void WithdrawMoney(Account account, decimal count)
        {
            CheckCurrency(account, count);

            account.Balance -= count;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

            DepositMoney(accountTo, count);
        }
    }
}
