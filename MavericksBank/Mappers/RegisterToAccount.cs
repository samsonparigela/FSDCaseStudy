using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class RegisterToAccount
	{
		Accounts account;
		public RegisterToAccount(AccountsCreateDTO createDTO)
		{
			account = new Accounts();
			account.AccountNumber = createDTO.AccountNumber;
			account.CustomerID = createDTO.customerID;
			account.Balance = createDTO.Balance;
			account.AccountType = createDTO.AccountType;
			account.IFSCCode = createDTO.IFSCCode;
		}
		public Accounts GetAccount()
		{
			return account;
		}
	}
}

