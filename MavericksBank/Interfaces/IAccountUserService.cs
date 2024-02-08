using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface IAccountUserService
	{
        public Task<AccountsCreateDTO> AddAccount(AccountsCreateDTO account);
        public Task<Accounts> GetAccountByID(int key);
        public Task<List<Accounts>> GetAllAccountsByUserID(int ID);
        public Task<AccountsUpdateDTO> UpdateAccount(AccountsUpdateDTO account);
        public Task<Accounts> DeleteAccount(int key);

    }
}

