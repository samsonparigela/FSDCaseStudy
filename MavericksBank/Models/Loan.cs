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
            float loanAmount, float interestRate, int tenureMonths, string status, string loanPurpose)
        {
            LoanID = loanID;
            CustomerID = customerID;
            LoanAmount = loanAmount;
            InterestRate = interestRate;
            TenureMonths = tenureMonths;
            Status = status;
            LoanPurpose = loanPurpose;
        }

        [Key]
        public int LoanID { set; get; }

        [ForeignKey("CustomerID")]
        public int CustomerID { set; get; }

        public Customer Customer { set; get; }

        public float LoanAmount { set; get; }
        public float InterestRate { set; get; }
        public int TenureMonths { set; get; }
        public string Status { set; get; }
        public string LoanPurpose { set; get; }

        public override string ToString()
        {
            return $"LoanID : {LoanID}\nCustomerID : {Customer.CustomerID}\nLoanAmount : {LoanAmount}\n InterestRate : {InterestRate}\n" +
            $"TenureMonths : {TenureMonths}\nStatus : {Status}\nLoanPurpose : {LoanPurpose}";
        }
    }
}

