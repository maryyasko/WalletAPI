using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.CustomException
{
    public class RateException : Exception
    {
        private const string DefaultMessage = "Невалидный тип валюты.";

        public RateException(string message)
        : base(message)
        { }

        public RateException()
        : base(DefaultMessage)
        { }
    }
}
