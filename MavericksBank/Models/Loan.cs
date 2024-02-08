using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Loan
	{
        public Loan()
        {
            this.Status = "Pending";
        }

        public Loan(int loanID, Customer customer, int customerID,
            float loanAmount, string status, string loanPurpose)
        {
            LoanID = loanID;
            CustomerID = customerID;
            LoanAmount = loanAmount;
            Status = status;
            LoanPurpose = loanPurpose;
        }

        [Key]
        public int LoanID { set; get; }

        [ForeignKey("CustomerID")]
        public int CustomerID { set; get; }

        public Customer? Customer { set; get; }

        [ForeignKey("LoanPloicyID")]
        public int LoanPolicyID { set; get; }

        public LoanPolicies? LoanPolicies { set; get; }

        public float LoanAmount { set; get; }
        public int TenureInMonths { set; get; }
        public float CalculateFinalAmount {
            get
            {
                return LoanAmount;
            }
            set
            {
                LoanAmount = LoanAmount * (1 + (LoanPolicies.Interest * LoanPolicies.TenureInMonths));
            }
        }


        public string Status { set; get; } = string.Empty;
        public string LoanPurpose { set; get; } = string.Empty;

        public override string ToString()
        {
            return $"LoanID : {LoanID}\nCustomerID : {Customer.CustomerID}\nLoanAmount : {LoanAmount}\n" +
            $"\nStatus : {Status}\nLoanPurpose : {LoanPurpose}";
        }
    }
}

