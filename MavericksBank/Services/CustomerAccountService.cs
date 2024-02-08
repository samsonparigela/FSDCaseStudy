using System;
using System.Security.Cryptography;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MavericksBank.Services
{
	public class CustomerAccountService:ICustomerAccountService
	{
        private readonly ILogger<CustomerAccountService> _logger;
        private readonly IRepository<Transactions, int> _TransacRepo;
        private readonly IRepository<Accounts, int> _AccRepo;

        public CustomerAccountService(ILogger<CustomerAccountService> logger,
            IRepository<Transactions, int> TransacRepo, IRepository<Accounts, int> AccRepo)
        {
            _logger = logger;
            _TransacRepo = TransacRepo;
            _AccRepo = AccRepo;

        }

        public async Task<AccountsCreateDTO> OpenAccount(AccountsCreateDTO account)
        {
            var myAccount = new RegisterToAccount(account).GetAccount();
            await _AccRepo.Add(myAccount);
            _logger.LogInformation("Account Created");
            return account;

        }

        public async Task<Accounts> CloseAccount(int key)
        {
            var account = await _AccRepo.GetByID(key);
            if(account.Status=="Close Approved")
            {
                account = await _AccRepo.Delete(key);
                _logger.LogInformation($"Successfully Deleted Account : {key}");
            }
            else
            {
                account.Status = "Close Request";
                _logger.LogInformation($"Request submitted to close Account : {key}");
                await _AccRepo.Update(account);
            }
            return account;
        }

        public async Task<AccountsUpdateDTO> EditAccount(AccountsUpdateDTO accountDTO)
        {
            var account = await _AccRepo.GetByID(accountDTO.ID);
            account.AccountNumber = accountDTO.AccountNumber;
            account.IFSCCode = accountDTO.IFSCCode;
            account.AccountType = accountDTO.AccountType;
            await _AccRepo.Update(account);
            _logger.LogInformation($"Successfully Updated Account with ID : {account.AccountID}");
            return accountDTO;
        }

        public async Task<Accounts> ViewAccountByID(int key)
        {
            var account = await _AccRepo.GetByID(key);
            _logger.LogInformation("Successfully Retrieved Account");
            return account;
        }

        public async Task<List<Transactions>> ViewAllTransactionBetweenDates(DateTime date1,DateTime date2,int ID)
        {
            var transactions = await ViewAllYourTransactions(ID);
            List<Transactions> transacBetweenDates = new List<Transactions>();
            foreach(Transactions transac in transactions)
            {
                if(transac.TransactionDate>=date1 && transac.TransactionDate<= date2)
                transacBetweenDates.Add(transac);
            }

            _logger.LogInformation($"All transactions retrieved between {date1} and {date2}");
            return transacBetweenDates;

        }

        public async Task<List<Transactions>> ViewAllYourTransactions(int CID)
        {
            var accounts = await ViewAllYourAccounts(CID);
            var transactions = await _TransacRepo.GetAll();

            List<int> AccountID = new List<int>();
            List<Transactions> AllTransactions = new List<Transactions>();

            foreach (Accounts acc in accounts)
            {
                AccountID.Add(acc.AccountID);
                AllTransactions.Concat(transactions.Where(t => t.SAccountID == acc.AccountID).ToList());
            }

            return AllTransactions;
        }

        public async Task<List<Transactions>> ViewAllTransactionsByAccount(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var transacByAccount = transactions.Where(t => t.SAccountID == AID).ToList();
            return transactions;
        }

        public async Task<List<Transactions>> ViewAllTransactionsMadeToAnAccount(int AID,int CID)
        {
            var transactions = await ViewAllYourTransactions(CID);
            List<Transactions> transacDoneToAcc = new List<Transactions>();
            foreach (Transactions transac in transactions)
            {
                if (transac.BeneficiaryID == AID)
                    transacDoneToAcc.Add(transac);
            }
            return transacDoneToAcc;
        }

        public async Task<List<Accounts>> ViewAllYourAccounts(int ID)
        {
            var accounts = await _AccRepo.GetAll();
            accounts = accounts.Where(d => d.CustomerID == ID).ToList();
            _logger.LogInformation($"Successfully Retrieved all accounts of Customer : {ID}");
            return accounts;
        }

        public async Task<List<Transactions>> ViewAllTransactionsInTheLastMonth(int CID)
        {
            var currentMonth = DateTime.Now.Month;
            var transactions = await ViewAllYourTransactions(CID);
            List<Transactions> transacDoneLastMonth = new List<Transactions>();
            foreach (Transactions transac in transactions)
            {
                if (transac.TransactionDate.Month<currentMonth)
                    transacDoneLastMonth.Add(transac);
            }
            return transacDoneLastMonth;
        }

        public async Task<List<Transactions>> ViewLastNTransactions(int ID,int n)
        {
            int c = 0;
            var transactions = await ViewAllYourTransactions(ID);
            var sortedTransac = transactions.OrderByDescending(t => t.TransactionDate).ToList();
            List<Transactions> LastMadeTransacs = new List<Transactions>();
            foreach(Transactions transac in sortedTransac)
            {
                LastMadeTransacs.Add(transac);
                c += 1;
                if (c == n)
                    break;
            }

            return LastMadeTransacs;
        }
    }
}

