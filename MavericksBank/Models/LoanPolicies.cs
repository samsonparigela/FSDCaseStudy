using System;
using System.ComponentModel.DataAnnotations;

namespace MavericksBank.Models
{
	public class LoanPolicies
	{
		public LoanPolicies()
		{

		}

        public LoanPolicies(int loanPolicyID, int tenureInMonths, float interest, float loanAmount)
        {
            LoanPolicyID = loanPolicyID;
            TenureInMonths = tenureInMonths;
            Interest = interest;
            LoanAmount = loanAmount;
        }

        [Key]
        public int LoanPolicyID { get; set; }
        public int TenureInMonths { get; set; }
        public float Interest { get; set; }
        public float LoanAmount { get; set; }

        public List<Loan>? Loans { get; set; }

    }
}

