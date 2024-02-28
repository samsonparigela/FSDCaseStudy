using System;
using System.Security.Cryptography;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Mvc;

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
                account.Status = "Account Closing Approved";
                await _AccRepo.Update(account);
                _logger.LogInformation("Account Closing Approved");
                return account;

        }
        public async Task<Customer> GetCustomerDetailsforAccount(int AID)
        {
            var accounts = await _AccRepo.GetAll();
            var account = accounts.SingleOrDefault(a => a.AccountNumber==AID);
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
            var filteredAccounts = accounts.Where(a => a.Status == "Close Request").ToList();
            _logger.LogInformation("Retrieved All Accounts with Close Request");
            return filteredAccounts;
        }

        public async Task<List<Accounts>> GetAllAccountsForOpenRequest()
        {
            var accounts = await _AccRepo.GetAll();
            var filteredAccounts = accounts.Where(a => a.Status == "Pending").ToList();
            _logger.LogInformation("Retrieved All Accounts with Open Request");
            return filteredAccounts;
        }

        public async Task<List<TransactionDTO>> GetAllTransactions()
        {
            var transactions = await _TransacRepo.GetAll();
            _logger.LogInformation("Retrieved All Transactions");

            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            foreach (Transactions transac in transactions)
            {
                var DTO = new GetTransactionDTO(transac).GetDTO();
                DTOList.Add(DTO);
            }
            return DTOList;
        }

        public async Task<Accounts> ViewAccountDetails(int AID)
        {
            var accounts =await _AccRepo.GetByID(AID);
            _logger.LogInformation("Retrieved Account Detals");
            return accounts;

        }

        public async Task<List<TransactionDTO>> ViewTransactionDetailsByAccount(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.Where(a=>a.SAccountID==AID).ToList();
            _logger.LogInformation($"Retrieved All Transactions of Account {AID}");
            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            foreach (Transactions transac in filteredtransactions)
            {
                var DTO = new GetTransactionDTO(transac).GetDTO();
                DTOList.Add(DTO);
            }
            return DTOList;
        }

        public async Task<List<TransactionDTO>> ViewSentTransactions(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.Where(t =>t.SAccountID == AID && t.TransactionType == "Sent").ToList();
            _logger.LogInformation($"Retrieved All Transactions of Account {AID}");
            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            foreach (Transactions transac in filteredtransactions)
            {
                var DTO = new GetTransactionDTO(transac).GetDTO();
                DTOList.Add(DTO);
            }
            return DTOList;
        }

        public async Task<List<TransactionDTO>> ViewReceivedTransactions(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.Where(t => t.SAccountID == AID && (t.TransactionType == "Deposit" || t.TransactionType=="Withdraw")).ToList();
            _logger.LogInformation($"Retrieved All Transactions of Account {AID}");
            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            foreach (Transactions transac in filteredtransactions)
            {
                var DTO = new GetTransactionDTO(transac).GetDTO();
                DTOList.Add(DTO);
            }
            return DTOList;
        }

        public async Task<List<TransactionDTO>> ViewTransactionsWith5HighestAmount()
        {
            var transactions = await _TransacRepo.GetAll();
            var filteredtransactions = transactions.OrderByDescending(t => t.Amount).ToList();
            _logger.LogInformation("Retrieved Top 5 Transactions");
            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            foreach (Transactions transac in filteredtransactions)
            {
                var DTO = new GetTransactionDTO(transac).GetDTO();
                DTOList.Add(DTO);
            }
            return DTOList;
        }
    }
}

