using System;
using MavericksBank.Models;

namespace MavericksBank.Interfaces
{
	public interface IBankEmpAccMngmtService
	{
        public Task<List<Accounts>> GetAllAccounts();
        public Task<List<Accounts>> GetAllAccountsForOpenRequest();
        public Task<List<Accounts>> GetAllAccountsForCloseRequest();
        public Task<List<Transactions>> GetAllTransactions();
        public Task<Accounts> ApproveAccountOpening(int AID);
		public Task<Accounts> ApproveAccountClosing(int AID);
		public Task<Accounts> ViewAccountDetails(int AID);
		public Task<List<Transactions>> ViewTransactionDetailsByAccount(int AID);
        public Task<List<Transactions>> ViewTransactionsWith5HighestAmount();
        public Task<Customer> GetCustomerDetailsforAccount(int AID);
        public Task<List<Transactions>> ViewReceivedTransactions(int AID);
        public Task<List<Transactions>> ViewSentTransactions(int AID);


    }
}

