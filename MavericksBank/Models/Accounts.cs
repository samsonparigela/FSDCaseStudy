using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Accounts:IEquatable<Accounts>
	{
        public Accounts()
        {
            Status = "Pending";
        }
        public Accounts(int accountID, int customerID, string accountType,
            float balance, string accountNumber,string status, string iFSCCode)
        {
            AccountID = accountID;
            CustomerID = customerID;
            AccountType = accountType;
            Balance = balance;
            AccountNumber = accountNumber;
            Status = status;
            IFSCCode = iFSCCode;
        }

        [Key]
        public int AccountID { set; get; }
        public string AccountNumber { set; get; }

        [ForeignKey("CustomerID")]
        public int CustomerID { set; get; }
        public Customer Customer { set; get; }
        //public Beneficiaries? Beneficiaries { set; get; }

        public string IFSCCode { set; get; }
        [ForeignKey("IFSCCode")]
        public Branches Branches { set; get; }


        
        public string Status { set; get; }
        public string AccountType { set; get; }
        public float Balance { set; get; }

        //Navigations

        public List<Transactions> RecievedTransactions { set; get; }

        public bool Equals(Accounts? other)
        {
            return this.AccountID == other.AccountID;
        }

        public override string ToString()
        {
            return $"AccountID : {AccountID}\nAccount Number: " +
                $"{AccountNumber}\nCustomerID : {Customer.CustomerID}\nAccountType : " +
                $"{AccountType}\nBalance : {Balance}\nStatus : {Status}";
        }
    }
}

