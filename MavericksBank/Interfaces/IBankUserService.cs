using System;
using MavericksBank.Models;

namespace MavericksBank.Interfaces
{
	public interface IBankUserService
	{
		public Task<Banks> GetBankbyID(int ID);
		public Task<List<Banks>> GetAllBanks();
	}
}

