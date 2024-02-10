using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : Controller
    {
        private readonly ILogger<CustomerAccountController> _logger;
        private readonly ICustomerAccountService _service;
        public CustomerAccountController(ILogger<CustomerAccountController> logger, ICustomerAccountService service)
        {
            _logger = logger;
            _service = service;
        }


        [Route("Open Account")]
        [HttpPost]
        public async Task<AccountsCreateDTO> AddAccount(AccountsCreateDTO account)
        {
            var myAccount = await _service.OpenAccount(account);
            _logger.LogInformation("Account Created");
            return myAccount;
        }

        [Route("Close Account")]
        [HttpDelete]
        public async Task<Accounts> CloseAccount(int key)
        {
            var account = await _service.CloseAccount(key);
            _logger.LogInformation("Account Created");
            return account;
        }

        [Route("Update Account")]
        [HttpPut]
        public async Task<AccountsUpdateDTO> UpdateAccount(AccountsUpdateDTO accountDTO)
        {
            var account = await _service.EditAccount(accountDTO);
            _logger.LogInformation("Account Updated");
            return account;
        }
        [Authorize(Roles = "Customer")]
        [Route("View All your Accounts")]
        [HttpGet]
        public async Task<List<Accounts>> ViewAllYourAccounts(int ID)
        {
            var account = await _service.ViewAllYourAccounts(ID);
            _logger.LogInformation($"Accounts Retrieved");
            return account;
        }

        [Route("View All Banks")]
        [HttpGet]
        public async Task<List<Banks>> ViewAllBanks()
        {
            var banks = await _service.ViewAllBanks();
            _logger.LogInformation($"Banks Retrieved");
            return banks;
        }

        [Route("View All Branches")]
        [HttpGet]
        public async Task<List<Branches>> ViewBankBranches(int BID)
        {
            var branches = await _service.ViewBankBranches(BID);
            _logger.LogInformation($"Branches Retrieved");
            return branches;
        }

        [Route("View Account By ID")]
        [HttpGet]
        public async Task<Accounts> ViewAccountByID(int key)
        {
            var account = await _service.ViewAccountByID(key);
            _logger.LogInformation($"Account {key} Retrieved");
            return account;
        }

        [Route("View Transactions By Dates")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewAllYourTransactions(int CID)
        {
            var transacs = await _service.ViewAllYourTransactions(CID);
            _logger.LogInformation("Transactions Retrieved");
            return transacs;
        }

        [Route("View all your Transactions")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewAllTransactionsByAccount(int AID)
        {
            var transacs = await _service.ViewAllTransactionsByAccount(AID);
            _logger.LogInformation($"Transactions of Account {AID} Retrieved");
            return transacs;
        }

        [Route("View all Transactions to an Account")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewAllTransactionsMadeToAnAccount(int AID, int CID)
        {
            var transacs = await _service.ViewAllTransactionsMadeToAnAccount(AID,CID);
            _logger.LogInformation($"Transactions for Account {CID} Retrieved");
            return transacs;
        }

        //[Route("View Transactions By Dates")]
        //[HttpGet]
        //public async Task<List<Transactions>> ViewAllTransactionBetweenDates(DateTime date1, DateTime date2, int ID)
        //{
        //    var transacs = await _service.ViewAllTransactionBetweenDates(date1, date2, ID);
        //    _logger.LogInformation($"Transactions between dates {date1} and {date2} Retrieved");
        //    return transacs;
        //}

        [Route("View Transactions in Last month")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewAllTransactionsInTheLastMonth(int CID)
        {
            var transacs = await _service.ViewAllTransactionsInTheLastMonth(CID);
            _logger.LogInformation($"Transactions made in the previous month Retrieved");
            return transacs;
        }

        [Route("View Transactions in This month")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewAllTransactionsInThisMonth(int CID)
        {
            var transacs = await _service.ViewAllTransactionsInThisMonth(CID);
            _logger.LogInformation($"Transactions made in this month Retrieved");
            return transacs;
        }

        [Route("View Last N Transactions")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewLastNTransactions(int ID, int n)
        {
            var transacs = await _service.ViewLastNTransactions(ID,n);
            _logger.LogInformation($"Last {n} Transactions Retrieved");
            return transacs;
        }
    }
}

