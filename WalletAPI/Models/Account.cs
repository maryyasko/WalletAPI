using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.Models
{
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
