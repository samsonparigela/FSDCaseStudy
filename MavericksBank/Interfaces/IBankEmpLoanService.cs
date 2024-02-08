using System;
using MavericksBank.Models;

namespace MavericksBank.Interfaces
{
	public interface IBankEmpLoanService
	{
		public Task<List<Loan>> GetAllLoansAppliedByACustomer(int CID);
        public Task<List<Loan>> GetAllLoansApplied();
        public Task<List<Loan>> GetAllLoansThatNeedApproval();
        public Task<bool> GetCustomerCreditworthiness(int CID);
        public Task<Loan> ApproveOrDisapproveLoan(int LID);

    }
}

