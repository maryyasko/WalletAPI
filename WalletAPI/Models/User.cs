using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Name { get; set; }

        public List<Account> Accounts { get; } = new List<Account>();

        public IEnumerable<string> GetAccountInfo()
        {
            var walletInfo = new List<string>();

            foreach (var currency in Accounts)
            {
                walletInfo.Add(currency.ToString());
            }

            return walletInfo;
        }
    }
}
