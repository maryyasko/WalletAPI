using System;

namespace WalletAPI.CustomException
{
    public class BalanceException : Exception
    {
        private const string DefaultMessage = "Недостаточно средств для снятия.";

        public BalanceException(string message)
        : base(message)
        { }

        public BalanceException()
        : base(DefaultMessage)
        { }
    }
}
