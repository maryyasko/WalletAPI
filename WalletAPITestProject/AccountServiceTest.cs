using Moq;
using WalletAPI.CustomException;
using WalletAPI.Loaders;
using WalletAPI.Services;
using Xunit;

namespace WalletAPITestProject
{
    /// <summary>
    /// “есты сервиса дл€ работы с счетами пользовател€.
    /// </summary>
    public class AccountServiceTest
    {
        [Fact]
        public void DepositMoney_Ok()
        {
            var account = TestDataClass.CreateAccount();
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            service.DepositMoney(account, 1000);

            Assert.Equal(2000, account.Balance);
        }

        [Fact]
        public void DepositMoney_Exception()
        {
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            Assert.ThrowsAny<AccountException>(() => service.DepositMoney(null, 1000));
        }

        [Fact]
        public void WithdrawMoney_Ok()
        {
            var account = TestDataClass.CreateAccount();
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            service.WithdrawMoney(account, 500);

            Assert.Equal(500, account.Balance);
        }

        [Fact]
        public void WithdrawMoney_Exception()
        {
            var account = TestDataClass.CreateAccount();
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            Assert.ThrowsAny<BalanceException>(() => service.WithdrawMoney(account, 2000));
        }

        [Fact]
        public void CreateAccount_Ok()
        {
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            var account = service.CreateAccount("USD");

            Assert.NotNull(account);
        }

        [Fact]
        public void CreateAccount_Exception()
        {
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            Assert.ThrowsAny<CurrencyException>(() => { var account = service.CreateAccount("eee"); });
        }

        [Fact]
        public void TransferMoney_Ok()
        {
            var account = TestDataClass.CreateAccount();
            var accountTo = TestDataClass.CreateAccountTo();
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            service.TransferMoney(account, accountTo, 500);

            Assert.Equal(500, account.Balance);
            Assert.Equal(22750, accountTo.Balance);
        }

        [Fact]
        public void TransferMoney_Exception()
        {
            var account = TestDataClass.CreateAccount();
            var currencyInformation = TestDataClass.GetCurrencyInformation();

            var mock = new Mock<IRateLoader>();
            mock.Setup(a => a.GetRateInformation()).Returns(currencyInformation);

            var service = new AccountService(mock.Object);

            Assert.ThrowsAny<AccountException>(() => service.TransferMoney(account, null, 1000));
        }
    }
}
