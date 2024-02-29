using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface ICustomerLoanService
	{
        public Task<LoanExtendDTO> AskForExtension(LoanExtendDTO loanExtend);
		public Task<LoanApplyDTO> ApplyForALoan(LoanApplyDTO loanApply);
        public Task<List<Loan>> GetAllAppliedLoans(int ID);
        public Task<List<LoanPolicies>> GetDifferentLoanPolicies();
        public Task<List<Loan>> GetAllAvailedLoans(int ID);
        public Task<List<Loan>> GetAllApprovedLoans(int ID);
        public Task<Loan> GetLoanByID(int ID);
        public Task<Loan> RepayLoan(int loanID, int accountNumber, int amount);
        public Task<Accounts> GetLoanAmountToAccount(int LoanID, int AccID);


    }
}

