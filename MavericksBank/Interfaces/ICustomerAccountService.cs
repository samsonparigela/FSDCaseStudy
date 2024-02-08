using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface ICustomerAccountService
	{
		public Task<AccountsCreateDTO> OpenAccount(AccountsCreateDTO accounts);
        public Task<Accounts> CloseAccount(int key);
        public Task<Accounts> ViewAccountByID(int key);
        public Task<AccountsUpdateDTO> EditAccount(AccountsUpdateDTO updateDTO);
        public Task<List<Accounts>> ViewAllYourAccounts(int ID);
        public Task<List<Transactions>> ViewAllYourTransactions(int ID);
        public Task<List<Transactions>> ViewAllTransactionBetweenDates(DateTime date1, DateTime date2, int ID);
        public Task<List<Transactions>> ViewAllTransactionsByAccount(int AID);
        public Task<List<Transactions>> ViewAllTransactionsMadeToAnAccount(int AID,int CID);
        public Task<List<Transactions>> ViewLastNTransactions(int ID, int n);
        public Task<List<Transactions>> ViewAllTransactionsInTheLastMonth(int CID);





    }
}

