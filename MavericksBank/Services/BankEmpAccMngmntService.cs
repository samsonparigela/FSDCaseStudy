using System;
using System.Security.Cryptography;
using MavericksBank.Interfaces;
using MavericksBank.Models;

namespace MavericksBank.Services
{
	public class BankEmpAccMngmntService: IBankEmpAccMngmtService
    {
        private readonly ILogger<BankEmpAccMngmntService> _logger;
        private readonly IRepository<Accounts, int> _AccRepo;
        private readonly IRepository<Transactions, int> _TransacRepo;
        private readonly IRepository<Customer, int> _CustRepo;
        public BankEmpAccMngmntService(ILogger<BankEmpAccMngmntService> logger,
            IRepository<Accounts, int> AccRepo, IRepository<Transactions, int> TransacRepo, IRepository<Customer, int> CustRepo)
		{
            _logger = logger;
            _AccRepo = AccRepo;
            _TransacRepo = TransacRepo;
            _CustRepo = CustRepo;
		}

        public async Task<Accounts> ApproveAccountClosing(int AID)
        {
            var account = await _AccRepo.GetByID(AID);
            await _AccRepo.Delete(account.AccountID);
            _logger.LogInformation("Account Closing Approved");
            return account;
        }
        public async Task<Customer> GetCustomerDetailsforAccount(int AID)
        {
            var accounts = await _AccRepo.GetAll();
            var account = accounts.SingleOrDefault(a => a.AccountID==AID);
            var customerID = account.CustomerID;
            var customer = await _CustRepo.GetByID(customerID);
            return customer;

        }
        public async Task<Accounts> ApproveAccountOpening(int AID)
        {
            var account = await _AccRepo.GetByID(AID);
            account.Status = "Approved";
            await _AccRepo.Update(account);
            _logger.LogInformation("Account Opening Approved");
            return account;

        }

        public async Task<List<Accounts>> GetAllAccounts()
        {
            var accounts = await _AccRepo.GetAll();
            _logger.LogInformation("Retrieved All Accounts");
            return accounts;
        }

        public async Task<List<Accounts>> GetAllAccountsForCloseRequest()
        {
            var accounts = await _AccRepo.GetAll();
            var filteredAccounts = accounts.Where(a => a.Status == "Close Request");
            _logger.LogInformation("Retrieved All Accounts with Close Request");
            return accounts;
        }

        public async Task<List<Accounts>> GetAllAccountsForOpenRequest()
        {
            var accounts = await _AccRepo.GetAll();
            var filteredAccounts = accounts.Where(a => a.Status == "Pending").ToList();
            _logger.LogInformation("Retrieved All Accounts with Open Request");
            return accounts;
        }

        public async Task<List<Transactions>> GetAllTransactions()
        {
            var transactions = await _TransacRepo.GetAll();
            _logger.LogInformation("Retrieved All Transactions");
            return transactions;
        }

        public async Task<Accounts> ViewAccountDetails(int AID)
        {
            var accounts =await _AccRepo.GetByID(AID);
            _logger.LogInformation("Retrieved Account Detals");
            return accounts;

        }

        public async Task<List<Transactions>> ViewTransactionDetailsByAccount(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.Where(a=>a.SAccountID==AID).ToList();
            _logger.LogInformation($"Retrieved All Transactions of Account {AID}");
            return filteredtransactions;
        }

        public async Task<List<Transactions>> ViewSentTransactions(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.Where(t => t.SAccountID==AID).ToList();
            _logger.LogInformation($"Retrieved All Transactions of Account {AID}");
            return filteredtransactions;
        }

        public async Task<List<Transactions>> ViewReceivedTransactions(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.Where(t => t.BeneficiaryID == AID).ToList();
            _logger.LogInformation($"Retrieved All Transactions of Account {AID}");
            return filteredtransactions;
        }

        public async Task<List<Transactions>> ViewTransactionsWith5HighestAmount()
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.OrderByDescending(t => t.Amount).ToList();
            _logger.LogInformation("Retrieved Top 5 Transactions");
            return filteredtransactions;
        }
    }
}

