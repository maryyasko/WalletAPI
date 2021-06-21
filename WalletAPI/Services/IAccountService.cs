using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.Models;

namespace WalletAPI.Services
{
    public interface IAccountService
    {
        void WithdrawMoney(Account account, decimal count);

        Account CreateAccount(string currancy);

        void DepositMoney(Account account, decimal count);

        void TransferMoney(Account accountFrom, Account accountTo, decimal count);
    }
}
