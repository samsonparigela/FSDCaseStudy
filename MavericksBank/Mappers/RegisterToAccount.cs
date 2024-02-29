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
			account = new Accounts(account.CustomerID,account.AccountType,account.Balance,
				account.AccountNumber,"Pending",account.IFSCCode)
			{

			};
        }
		public Accounts GetAccount()
		{
			return account;
		}
	}
}

