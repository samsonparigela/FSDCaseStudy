using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class AddToLoan
	{
		Loan loan;
		public AddToLoan(LoanApplyDTO loanApply)
		{
			loan = new Loan();
			loan.CustomerID = loanApply.CustomerID;
			loan.LoanAmount = loanApply.LoanAmount;
			loan.LoanPolicyID = loanApply.LoanPolicyID;
			loan.LoanPurpose = loanApply.LoanPurpose;

		}

		public Loan GetLoan()
		{
			return loan;
		}
    }
}

