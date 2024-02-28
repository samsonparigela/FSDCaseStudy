using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class CustomerAccountController : Controller
    {
        private readonly ILogger<CustomerAccountController> _logger;
        private readonly ICustomerAccountService _service;
        public CustomerAccountController(ILogger<CustomerAccountController> logger, ICustomerAccountService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [Route("OpenAccount")]
        [HttpPost]
        public async Task<ActionResult<AccountsCreateDTO>> AddAccount(AccountsCreateDTO account)
        {
            try
            {
                var myAccount = await _service.OpenAccount(account);
                _logger.LogInformation("Account Created");
                return myAccount;
            }
            catch(AccountAlreadyPresentException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("CloseAccount")]
        [HttpDelete]
        public async Task<ActionResult<Accounts>> CloseAccount(int key)
        {
            try
            {
                var account = await _service.CloseAccount(key);
                _logger.LogInformation("Account Created");
                return account;
            }
            catch(AccountDeletionException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("Update Account")]
        [HttpPut]
        public async Task<ActionResult<AccountsUpdateDTO>> UpdateAccount(AccountsUpdateDTO accountDTO)
        {
            try
            {
                var account = await _service.EditAccount(accountDTO);
                _logger.LogInformation("Account Updated");
                return account;
            }
            catch(NoAccountFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("View All your Accounts")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> ViewAllYourAccounts(int ID)
        {
            try
            {
                var account = await _service.ViewAllYourAccounts(ID);
                _logger.LogInformation($"Accounts Retrieved");
                return account;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("View All Banks")]
        [HttpGet]
        public async Task<ActionResult<List<Banks>>> ViewAllBanks()
        {
            try
            {
                var banks = await _service.ViewAllBanks();
                _logger.LogInformation($"Banks Retrieved");
                return banks;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("View All Branches")]
        [HttpGet]
        public async Task<ActionResult<List<Branches>>> ViewBankBranches(int BID)
        {
            try
            {
                var branches = await _service.ViewBankBranches(BID);
                _logger.LogInformation($"Branches Retrieved");
                return branches;
            }
            catch (NoBranchFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("View Account By ID")]
        [HttpGet]
        public async Task<ActionResult<Accounts>> ViewAccountByID(int key)
        {
            try
            {
                var account = await _service.ViewAccountByID(key);
                _logger.LogInformation($"Account {key} Retrieved");
                return account;
            }
            catch (NoAccountFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("ViewAllYourTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewAllYourTransactions(int CID)
        {
            try
            {
                var transacs = await _service.ViewAllYourTransactions(CID);
                _logger.LogInformation("Transactions Retrieved");
                return transacs;
            }
            catch (NoAccountFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }


        [Route("ViewAllYourTransactionsByAccount")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewAllTransactionsByAccount(int AID)
        {
            try
            {
                var transacs = await _service.ViewAllTransactionsByAccount(AID);
                _logger.LogInformation($"Transactions of Account {AID} Retrieved");
                return transacs;
            }
            catch (AccountTransactionException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("View all Transactions to an Account")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewAllTransactionsMadeToAnAccount(int AID, int CID)
        {
            try
            {
                var transacs = await _service.ViewAllTransactionsMadeToAnAccount(AID, CID);
                _logger.LogInformation($"Transactions for Account {CID} Retrieved");
                return transacs;
            }
            catch (AccountTransactionException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
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
        public async Task<ActionResult<List<TransactionDTO>>> ViewAllTransactionsInTheLastMonth(int CID)
        {
            try
            {
                var transacs = await _service.ViewAllTransactionsInTheLastMonth(CID);
                _logger.LogInformation($"Transactions made in the previous month Retrieved");
                return transacs;
            }
            catch (AccountTransactionException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("View Transactions in This month")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewAllTransactionsInThisMonth(int CID)
        {
            try
            {
                var transacs = await _service.ViewAllTransactionsInThisMonth(CID);
                _logger.LogInformation($"Transactions made in this month Retrieved");
                return transacs;
            }
            catch (AccountTransactionException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("View Last N Transactions")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewLastNTransactions(int ID, int n)
        {
            try
            {
                var transacs = await _service.ViewLastNTransactions(ID, n);
                _logger.LogInformation($"Last {n} Transactions Retrieved");
                return transacs;
            }
            catch (AccountTransactionException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }
    }
}

