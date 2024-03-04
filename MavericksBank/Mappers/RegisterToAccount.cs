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
			account = new Accounts(createDTO.customerID, createDTO.AccountType, createDTO.Balance,
                createDTO.AccountNumber, "Pending", createDTO.IFSCCode);
        }
		public Accounts GetAccount()
		{
			return account;
		}
	}
}

