using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTransactionController : Controller
    {
        private readonly ILogger<CustomerTransactionController> _logger;
        private readonly ICustomerTransactionService _service;
        public CustomerTransactionController(ILogger<CustomerTransactionController> logger, ICustomerTransactionService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("Deposit Money")]
        [HttpPost]
        public async Task<Transactions> DepositMoney(int accountNumber, int amount)
        {
            var transaction =await _service.DepositMoney(accountNumber, amount);
            _logger.LogInformation("Money Deposited");
            return transaction;
        }

        [Route("Transfer Money")]
        [HttpPost]
        public async Task<Transactions> TransferMoney(int amount, int destAccountID, int accountNumber)
        {
            var transaction = await _service.TransferMoney(amount, destAccountID, accountNumber);
            _logger.LogInformation("Money Transferred");
            return transaction;
        }

        [Route("Withdraw Money")]
        [HttpPost]
        public async Task<Transactions> WithdrawMoney(int amount, int accountID)
        {
            var transaction = await _service.WithdrawMoney(amount, accountID);
            _logger.LogInformation("Money Withdrawn");
            return transaction;
        }

        [Route("Get All Transactions")]
        [HttpGet]
        public async Task<List<Transactions>> GetAllTransactions(int AID)
        {
            var transaction = await _service.GetAllTransactions(AID);
            _logger.LogInformation("All Transactions retrieved");
            return transaction;
        }

        [Route("Get Transactions ByID")]
        [HttpGet]
        public async Task<Transactions> GetTransactionsByID(int TID)
        {
            var transaction = await _service.GetTransactionsByID(TID);
            _logger.LogInformation($"Transaction {TID} retrieved");
            return transaction;
        }
    }
}

