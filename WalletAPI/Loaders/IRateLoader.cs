using System.Collections.Generic;
using WalletAPI.Models;

namespace WalletAPI.Loaders
{
    /// <summary>
    /// Загрузчик информации о текущем курсе валют.
    /// </summary>
    public interface IRateLoader
    {
        /// <summary>
        /// Получить информацию о текущем курсе валют.
        /// </summary>
        /// <returns>Информация о текущем курсе валют.</returns>
        public IEnumerable<CurrencyInformation> GetRateInformation();
    }
}
