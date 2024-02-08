using System;
using MavericksBank.Models;

namespace MavericksBank.Interfaces
{
	public interface ICustomerTransactionService
	{
		public Task<Transactions> TransferMoney(int amount,int destAccountID, string sourceAccountID);
        public Task<Transactions> WithdrawMoney(int amount,int accountID);
        public Task<Transactions> DepositMoney(string accountNumber, int amount);
        public Task<List<Transactions>> GetAllTransactions(int CID);
        public Task<Transactions> GetTransactionsByID(int TID);


    }
}

