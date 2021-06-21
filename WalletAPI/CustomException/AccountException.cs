using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.CustomException
{
    public class AccountException : Exception
    {
        private const string DefaultMessage = "Счета в заданной валюте не существует. Откройте счет.";

        public AccountException(string message)
        : base(message)
        { }

        public AccountException()
        : base(DefaultMessage)
        { }
    }
}
