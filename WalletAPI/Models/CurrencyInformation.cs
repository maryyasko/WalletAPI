using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.Models
{
    public class CurrencyInformation
    {
        public string Currency { get; set; }

        public decimal Rate { get; set; }
    }
}
