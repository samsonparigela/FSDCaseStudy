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
        public Task<LoanPolicies> AddLoanPolicies(LoanPolicies policies);
        public Task<List<LoanPolicies>> GetDifferentLoanPolicies();
        public Task<LoanPolicies> DeleteLoanPolicies(int ID);
        public Task<LoanPolicies> UpdateLoanPolicies(LoanPolicies policies);
        public Task<Loan> ApproveOrDisapproveLoanExtend(int LID, string approval);

    }
}

