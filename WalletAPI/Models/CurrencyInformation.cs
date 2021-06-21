namespace WalletAPI.Models
{
    /// <summary>
    /// Информация о валюте.
    /// </summary>
    public class CurrencyInformation
    {
        public string Currency { get; set; }

        public decimal Rate { get; set; }
    }
}
