using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.CustomException
{
    public class CurrencyException : Exception
    {
        private const string DefaultMessage = "Недостаточно средств для снятия.";

        public CurrencyException(string message)
        : base(message)
        { }

        public CurrencyException()
        : base(DefaultMessage)
        { }
    }
}
