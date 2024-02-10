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
        public Task<List<TransactionDTO>> ViewAllYourTransactions(int ID);
//      public Task<List<TransactionDTO>> ViewAllTransactionBetweenDates(DateTime date1, DateTime date2, int ID);
        public Task<List<TransactionDTO>> ViewAllTransactionsByAccount(int AID);
        public Task<List<TransactionDTO>> ViewAllTransactionsMadeToAnAccount(int AID,int CID);
        public Task<List<TransactionDTO>> ViewLastNTransactions(int ID, int n);
        public Task<List<TransactionDTO>> ViewAllTransactionsInTheLastMonth(int CID);
        public Task<List<TransactionDTO>> ViewAllTransactionsInThisMonth(int CID);
        public Task<List<Banks>> ViewAllBanks();
        public Task<List<Branches>> ViewBankBranches(int BID);





    }
}

