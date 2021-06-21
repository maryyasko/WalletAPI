using System;

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
