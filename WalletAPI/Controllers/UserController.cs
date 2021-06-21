using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.CustomException;
using WalletAPI.Loaders;
using WalletAPI.Models;
using WalletAPI.Services;

namespace WalletAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly WalletContext _context;

        private AccountService AccountService;

        private UserService UserService;

        public UserController(WalletContext context, IRateLoader rateLoader)
        {
            _context = context;
            AccountService = new AccountService(rateLoader);
            UserService = new UserService(context, rateLoader);
        }

        [HttpPut]
        [Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(string userName)
        {
            try
            {
                UserService.CreateUser(userName);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await UserService.GetUsers().ConfigureAwait(false);

            return Ok(users);
        }

        [HttpGet]
        [Route("GetAccountsInfo")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountsInfo(string userName)
        {
            try
            {
                var accountInfo = await UserService.GetAccountInfo(userName).ConfigureAwait(false);

                return Ok(accountInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("CreateAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount(string userName, string currency)
        {
            try
            {
                await UserService.CreateAccount(userName, currency).ConfigureAwait(false);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddMoney")]
        [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DepositMoney(string userName, string currency, decimal count)
        {
            try
            {
                await UserService.DepositMoney(userName, currency, count).ConfigureAwait(false);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("WithdrawMoney")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> WithdrawMoney(string userName, string currency, decimal count)
        {
            try
            {
                await UserService.WithdrawMoney(userName, currency, count).ConfigureAwait(false);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("TransferMoney")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TransferMoney(string userName, string currencyFrom, string currencyTo, decimal count)
        {
            try
            {
                await UserService.TransferMoneyAsync(userName, currencyFrom, currencyTo, count).ConfigureAwait(false);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
