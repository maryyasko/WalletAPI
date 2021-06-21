using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.CustomException
{
    public class CurrencyException : Exception
    {
        private const string DefaultMessage = "Невалидный тип валюты.";

        public CurrencyException(string message)
        : base(message)
        { }

        public CurrencyException()
        : base(DefaultMessage)
        { }
    }
}
