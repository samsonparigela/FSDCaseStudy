using System;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace MavericksBank.Services
{
	public class CustomerTransactionService:ICustomerTransactionService
	{
        private readonly ILogger<CustomerTransactionService> _logger;
        private readonly IRepository<Transactions, int> _TransacRepo;
        private readonly IRepository<Accounts, int> _AccRepo;
        public CustomerTransactionService(ILogger<CustomerTransactionService> logger,
            IRepository<Transactions, int> TransacRepo, IRepository<Accounts, int> AccRepo)
		{
            _logger = logger;
            _TransacRepo = TransacRepo;
            _AccRepo = AccRepo;
		}
        public async Task<Accounts> GetByAccountNumber(int accountNumber)
        {
            var account = await _AccRepo.GetAll();
            var myAccount = account.SingleOrDefault(p => p.AccountNumber == accountNumber);
            return myAccount;

        }

        public async Task<List<Transactions>> GetAllTransactions(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            transactions = transactions.Where(d => d.SAccountID == AID).ToList();
            _logger.LogInformation("All transactions retrieved");
            return transactions;

        }

        public async Task<Transactions> GetTransactionsByID(int TID)
        {
            var transaction = await _TransacRepo.GetByID(TID);
            _logger.LogInformation($"Transaction {TID} Retrieved");
            return transaction;
        }


        public async Task<Transactions> DepositMoney(int accountNumber, int amount)
        {
            var account = await GetByAccountNumber(accountNumber);
            if (account.Status == "Pending")
                throw new AccountTransactionException("Account is not active yet");
            account.Balance += amount;
            await _AccRepo.Update(account);
            var transaction = new Transactions();
            transaction.Amount = amount;
            transaction.TransactionType = "Deposit";
            transaction.TransactionDate = DateTime.Now;
            transaction.Status = "Success";
            transaction.SAccountID = account.AccountNumber;
            transaction.BeneficiaryAccountNumber = account.AccountNumber;
            transaction.Description = "Self Deposit";

            transaction = await _TransacRepo.Add(transaction);
            _logger.LogInformation("Amount Deposited");
            return transaction;

        }

        public async Task<Transactions> TransferMoney(int amount, int destAccountID, int accountNumber)
        {
            var account = await GetByAccountNumber(accountNumber);
            if (account.Status == "Pending")
                throw new AccountTransactionException("Account is not active yet");
            if (amount > account.Balance)
                throw new InsufficientFundsException("No Sufficient Funds to do the transaction");
            account.Balance -= amount;
            await _AccRepo.Update(account);
            var transaction1 = new Transactions();
            transaction1.Amount = amount;
            transaction1.TransactionType = "Sent";
            transaction1.TransactionDate = DateTime.Now;
            transaction1.Status = "Success";
            transaction1.SAccountID = accountNumber;
            transaction1.BeneficiaryAccountNumber = destAccountID;
            transaction1.Description = "Sent";

            _logger.LogInformation("Amount Transferred");

            transaction1 = await _TransacRepo.Add(transaction1);
            return transaction1;
        }

        public async Task<Transactions> WithdrawMoney(int amount,int accountID)
        {
            var account = await _AccRepo.GetByID(accountID);
            if (account.Status == "Pending")
                throw new AccountTransactionException("Account is not active yet");
            if (amount > account.Balance)
                throw new InsufficientFundsException("No Sufficient Funds to do the transaction");
            account.Balance -= amount;
            await _AccRepo.Update(account);
            var transaction = new Transactions();
            transaction.Amount = amount;
            transaction.TransactionType = "Withdraw";
            transaction.TransactionDate = DateTime.Now;
            transaction.Status = "Success";
            transaction.SAccountID = accountID;
            transaction.BeneficiaryAccountNumber= account.AccountNumber;
            transaction.Description = "Self Withdraw";

            _logger.LogInformation("Amount Withdrawn");

            transaction = await _TransacRepo.Add(transaction);

            return transaction;
        }
    }
}

