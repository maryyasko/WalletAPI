using WalletAPI.Models;

namespace WalletAPI.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Снять деньги со счета.
        /// </summary>
        /// <param name="account">Счет.</param>
        /// <param name="count">Сумма для снятия.</param>
        void WithdrawMoney(Account account, decimal count);

        /// <summary>
        /// Открыть новый счет.
        /// </summary>
        /// <param name="currancy">Валюта, в которой требуется открыть счет.</param>
        /// <returns>Открытый счет.</returns>
        Account CreateAccount(string currancy);

        /// <summary>
        /// Внести деньги на счет.
        /// </summary>
        /// <param name="account">Счет.</param>
        /// <param name="count">Сумма для внесения.</param>
        void DepositMoney(Account account, decimal count);

        /// <summary>
        /// Перевести деньги с одного счета на другой.
        /// </summary>
        /// <param name="accountFrom">Исходный счет.</param>
        /// <param name="accountTo">Счет, на который нужно перевести деньги.</param>
        /// <param name="count">Сумма перевода.</param>
        void TransferMoney(Account accountFrom, Account accountTo, decimal count);
    }
}
