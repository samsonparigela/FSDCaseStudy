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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class BankEmpAccountController : Controller
    {
        private readonly ILogger<BankEmpAccountController> _logger;
        private readonly IBankEmpAccMngmtService _service;
        public BankEmpAccountController(ILogger<BankEmpAccountController> logger, IBankEmpAccMngmtService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Bank Employee")]
        [Route("ApproveAccountClosing")]
        [HttpGet]
        public async Task<ActionResult<Accounts>> ApproveAccountClosing(int AID)
        {
            try
            {
                var account = await _service.ApproveAccountClosing(AID);
                return account;
            }
            catch(NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetCustomerDetailsForAccount")]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomerDetailsforAccount(int AID)
        {
            try
            { 
            var customer = await _service.GetCustomerDetailsforAccount(AID);
            return customer;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee")]
        [Route("ApproveAccountOpening")]
        [HttpGet]
        public async Task<ActionResult<Accounts>> ApproveAccountOpening(int AID)
        {
            try
            {
                var account = await _service.ApproveAccountOpening(AID);
                return account;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetAllAccounts")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllAccounts()
        {
            try
            {
                var accounts = await _service.GetAllAccounts();
                return accounts;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetAllAccountsForCloseRequest")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllAccountsForCloseRequest()
        {
            try
            {
                var accounts = await _service.GetAllAccountsForCloseRequest();
                return accounts;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetAllAccountsForOpenRequest")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllAccountsForOpenRequest()
        {
            try
            {
                var accounts = await _service.GetAllAccountsForOpenRequest();
                return accounts;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetAllTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> GetAllTransactions()
        {
            try
            {
                var transactions = await _service.GetAllTransactions();
                return transactions;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("ViewAccountDetails")]
        [HttpGet]
        public async Task<ActionResult<Accounts>> ViewAccountDetails(int AID)
        {
            try
            {
                var account = await _service.ViewAccountDetails(AID);
                return account;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("ViewTransactionDetailsByAccount")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewTransactionDetailsByAccount(int AID)
        {
            try
            {
                var transactions = await _service.ViewTransactionDetailsByAccount(AID);
                return transactions;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin,Customer")]
        [Route("ViewSentTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewSentTransactions(int AID)
        {
            try
            {
                var transactions = await _service.ViewSentTransactions(AID);
                return transactions;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }

        [Authorize(Roles = "Bank Employee,Admin,Customer")]
        [Route("ViewReceivedTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewReceivedTransactions(int AID)
        {
            try
            {
                var transactions = await _service.ViewReceivedTransactions(AID);
                return transactions;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("ViewTransactionsWith5HighestAmount")]
        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> ViewTransactionsWith5HighestAmount()
        {
            try
            {
                var transactions = await _service.ViewTransactionsWith5HighestAmount();
                return transactions;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }


    }
}

