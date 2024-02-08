using System;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MavericksBank.Services
{
	public class AccountService : IAccountAdminService
	{
        private readonly ILogger<AccountService> _logger;
        private readonly IRepository<Accounts, int> _AccRepo;
        public AccountService(ILogger<AccountService> logger, IRepository<Accounts, int> AccRepo)
		{
            _logger = logger;
            _AccRepo = AccRepo;
		}

        public async Task<AccountsCreateDTO> AddAccount(AccountsCreateDTO account)
        {
            var myAccount = new RegisterToAccount(account).GetAccount();
             await _AccRepo.Add(myAccount);
            _logger.LogInformation("Account Created");
            return account;

        }

        public async Task<Accounts> DeleteAccount(int key)
        {
            var account = await _AccRepo.Delete(key);
            _logger.LogInformation($"Successfully Deleted Account : {key}");
            return account;
        }

        public async Task<Accounts> GetAccountByID(int key)
        {
            var account = await _AccRepo.GetByID(key);
            _logger.LogInformation("Successfully Retrieved Account");
            return account;
        }

        public async Task<List<Accounts>> GetAllAccountsByUserID(int ID)
        {
            var accounts = await _AccRepo.GetAll();
            accounts = accounts.Where(d => d.CustomerID == ID).ToList();
            _logger.LogInformation($"Successfully Retrieved all accounts of Customer : {ID}");
            return accounts;
        }

        public async Task<List<Accounts>> GetAllAccounts()
        {
            var accounts = await _AccRepo.GetAll();
            _logger.LogInformation($"Successfully Retrieved all accounts in the Database");
            return accounts;
        }

        public async Task<AccountsUpdateDTO> UpdateAccount(AccountsUpdateDTO accountDTO)
        {
            var account = await _AccRepo.GetByID(accountDTO.ID);
            account.AccountNumber = accountDTO.AccountNumber;
            account.IFSCCode = accountDTO.IFSCCode;
            account.AccountType = accountDTO.AccountType;
            await _AccRepo.Update(account);
            _logger.LogInformation($"Successfully Updated Account with ID : {account.AccountID}");
            return accountDTO;
        }

        public async Task<AccountStatusUpdateDTO> UpdateAccountStatus(AccountStatusUpdateDTO accountDTO)
        {
            var account = await _AccRepo.GetByID(accountDTO.ID);
            account.Status = accountDTO.Status;
            await _AccRepo.Update(account);
            _logger.LogInformation($"Successfully Updated Account Status with ID : {account.AccountID}");
            return accountDTO;
        }
    }
}

