using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface IAccountAdminService:IAccountUserService
    {
        public Task<AccountStatusUpdateDTO> UpdateAccountStatus(AccountStatusUpdateDTO account);
        public Task<List<Accounts>> GetAllAccounts();
    }
}

