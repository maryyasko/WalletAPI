using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.CustomException
{
    public class UserWasNullException : Exception
    {
        private const string DefaultMessage = "Пользователь с таким именем не найден.";

        public UserWasNullException (string message)
        : base(message)
        { }

        public UserWasNullException()
        : base(DefaultMessage)
        { }
    }
}
