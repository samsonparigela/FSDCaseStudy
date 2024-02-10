using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface IBankEmpAccMngmtService
	{
        public Task<List<Accounts>> GetAllAccounts();
        public Task<List<Accounts>> GetAllAccountsForOpenRequest();
        public Task<List<Accounts>> GetAllAccountsForCloseRequest();
        public Task<List<TransactionDTO>> GetAllTransactions();
        public Task<Accounts> ApproveAccountOpening(int AID);
		public Task<Accounts> ApproveAccountClosing(int AID);
		public Task<Accounts> ViewAccountDetails(int AID);
		public Task<List<TransactionDTO>> ViewTransactionDetailsByAccount(int AID);
        public Task<List<TransactionDTO>> ViewTransactionsWith5HighestAmount();
        public Task<Customer> GetCustomerDetailsforAccount(int AID);
        public Task<List<TransactionDTO>> ViewReceivedTransactions(int AID);
        public Task<List<TransactionDTO>> ViewSentTransactions(int AID);


    }
}

