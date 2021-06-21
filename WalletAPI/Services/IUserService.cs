using System.Collections.Generic;
using System.Threading.Tasks;

namespace WalletAPI.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Создать нового пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        void CreateUser(string userName);

        /// <summary>
        /// Получить список существующих пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        Task<List<string>> GetUsers();

        /// <summary>
        /// Получить информацию о счетах пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Информация о счетах.</returns>
        Task<IEnumerable<string>> GetAccountInfo(string userName);

        /// <summary>
        /// Создать новый счет.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="currency">Тип валюты.</param>
        Task CreateAccount(string userName, string currency);

        /// <summary>
        /// Пополнить счет.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="currency">Тип счета для пополнения.</param>
        /// <param name="count">Сумма пополнения.</param>
        Task DepositMoney(string userName, string currency, decimal count);

        /// <summary>
        /// Снять деньги со счета.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="currency">Тип счета для снятия.</param>
        /// <param name="count">Сумма снятия.</param>
        Task WithdrawMoney(string userName, string currency, decimal count);

        /// <summary>
        /// Перевести деньги со счета на другой счет.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="currencyFrom">Счет, с которого требуется перевести деньги.</param>
        /// <param name="currencyTo">Счет, на который требуется перевести деньги.</param>
        /// <param name="count">Сумма перевода.</param>
        Task TransferMoney(string userName, string currencyFrom, string currencyTo, decimal count);
    }
}
