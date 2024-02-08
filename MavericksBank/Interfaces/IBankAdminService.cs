using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface IBankAdminService:IBankUserService
	{
		public Task<BankCreateDTO> AddBank(BankCreateDTO bank);
		public Task<BankUpdateDTO> UpdateBank(BankUpdateDTO bank);
		public Task<Banks> DeleteBank(int bankID);
	}
}

