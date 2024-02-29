using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class CustomerTransactionController : Controller
    {
        private readonly ILogger<CustomerTransactionController> _logger;
        private readonly ICustomerTransactionService _service;
        public CustomerTransactionController(ILogger<CustomerTransactionController> logger, ICustomerTransactionService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [Route("Deposit Money")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> DepositMoney(int accountNumber, int amount)
        {
            try
            {
                var transaction = await _service.DepositMoney(accountNumber, amount);
                _logger.LogInformation("Money Deposited");
                return transaction;
            }
            catch(AccountTransactionException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("Transfer Money")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> TransferMoney(int amount, int destAccountID, int accountNumber)
        {
            try
            {
                var transaction = await _service.TransferMoney(amount, destAccountID, accountNumber);
                _logger.LogInformation("Money Transferred");
                return transaction;
            }
            catch (AccountTransactionException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (InsufficientFundsException ex)
            {
                string msg = ex.Message;
                _logger.LogCritical(ex.Message);
                return BadRequest(msg);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("Withdraw Money")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> WithdrawMoney(int amount, int accountID)
        {
            try
            {
                var transaction = await _service.WithdrawMoney(amount, accountID);
                _logger.LogInformation("Money Withdrawn");
                return transaction;
            }
            catch (AccountTransactionException ex)
            {
                string msg = ex.Message;
                _logger.LogCritical(ex.Message);
                return BadRequest(msg);
            }
            catch (InsufficientFundsException ex)
            {
                string msg = ex.Message;
                _logger.LogCritical(ex.Message);
                return BadRequest(msg);
            }
        }

        [Route("Get All Transactions")]
        [HttpGet]
        public async Task<ActionResult<List<Transactions>>> GetAllTransactions(int AID)
        {
            try
            {
                var transaction = await _service.GetAllTransactions(AID);
                _logger.LogInformation("All Transactions retrieved");
                return transaction;
            }
            catch (NoAccountFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("Get Transactions ByID")]
        [HttpGet]
        public async Task<ActionResult<Transactions>> GetTransactionsByID(int TID)
        {
            try
            {
                var transaction = await _service.GetTransactionsByID(TID);
                _logger.LogInformation($"Transaction {TID} retrieved");
                return transaction;
            }
            catch (NoTransactionsFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

