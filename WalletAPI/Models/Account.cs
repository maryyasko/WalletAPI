using System.ComponentModel.DataAnnotations.Schema;

namespace WalletAPI.Models
{
    /// <summary>
    /// Счет.
    /// </summary>
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        public string Currency { get; set; }

        public decimal Balance { get; set; }

        public string ToString()
        {
            return $"{Balance} {Currency}";
        }
    }
}
