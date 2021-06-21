using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.Models;

namespace WalletAPI.Loaders
{
    public interface IRateLoader
    {
        public IEnumerable<CurrencyInformation> GetRateInformation();
    }
}
