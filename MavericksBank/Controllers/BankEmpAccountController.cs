using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankEmpAccountController : Controller
    {
        private readonly ILogger<BankEmpAccountController> _logger;
        private readonly IBankEmpAccMngmtService _service;
        public BankEmpAccountController(ILogger<BankEmpAccountController> logger, IBankEmpAccMngmtService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("Approve Account Closing")]
        [HttpGet]
        public async Task<Accounts> ApproveAccountClosing(int AID)
        {
            var account = await _service.ApproveAccountClosing(AID);
            return account;
        }

        [Route("Get Customer Details for Account")]
        [HttpGet]
        public async Task<Customer> GetCustomerDetailsforAccount(int AID)
        {
            var customer = await _service.GetCustomerDetailsforAccount(AID);
            return customer;
        }

        [Route("Approve Account Opening")]
        [HttpGet]
        public async Task<Accounts> ApproveAccountOpening(int AID)
        {
            var account = await _service.ApproveAccountOpening(AID);
            return account;
        }

        [Route("Get All Accounts")]
        [HttpGet]
        public async Task<List<Accounts>> GetAllAccounts()
        {
            var accounts = await _service.GetAllAccounts();
            return accounts;
        }

        [Route("Get All Accounts For CloseRequest")]
        [HttpGet]
        public async Task<List<Accounts>> GetAllAccountsForCloseRequest()
        {
            var accounts = await _service.GetAllAccountsForCloseRequest();
            return accounts;
        }

        [Route("Get All Accounts For OpenRequest")]
        [HttpGet]
        public async Task<List<Accounts>> GetAllAccountsForOpenRequest()
        {
            var accounts = await _service.GetAllAccountsForOpenRequest();
            return accounts;
        }

        [Route("Get All Transactions")]
        [HttpGet]
        public async Task<List<TransactionDTO>> GetAllTransactions()
        {
            var transactions = await _service.GetAllTransactions();
            return transactions;
        }

        [Route("View Account Details")]
        [HttpGet]
        public async Task<Accounts> ViewAccountDetails(int AID)
        {
            var account = await _service.ViewAccountDetails(AID);
            return account;
        }

        [Route("View Transaction Details By Account")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewTransactionDetailsByAccount(int AID)
        {
            var transactions = await _service.ViewTransactionDetailsByAccount(AID);
            return transactions;
        }

        [Route("View Sent Transactions")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewSentTransactions(int AID)
        {
            var transactions = await _service.ViewSentTransactions(AID);
            return transactions;

        }

        [Route("View Received Transactions")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewReceivedTransactions(int AID)
        {
            var transactions = await _service.ViewReceivedTransactions(AID);
            return transactions;
        }

        [Route("View Transactions With 5 Highest Amount")]
        [HttpGet]
        public async Task<List<TransactionDTO>> ViewTransactionsWith5HighestAmount()
        {
            var transactions = await _service.ViewTransactionsWith5HighestAmount();
            return transactions;
        }


    }
}

