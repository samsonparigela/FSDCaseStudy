using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Accounts:IEquatable<Accounts>
	{
        public Accounts()
        {

        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int AccountNumber { set; get; }

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

        public Accounts(int customerID, string accountType,
    float balance, int accountNumber, string status, string iFSCCode)
        {
            CustomerID = customerID;
            AccountType = accountType;
            Balance = balance;
            AccountNumber = accountNumber;
            Status = status;
            IFSCCode = iFSCCode;
        }

        //Navigations

        public List<Transactions> RecievedTransactions { set; get; }

        public bool Equals(Accounts? other)
        {
            return this.AccountNumber == other.AccountNumber;
        }

        public override string ToString()
        {
            return $"Account Number: {AccountNumber}" +
                $"\nCustomerID : {Customer.CustomerID}\nAccountType : " +
                $"{AccountType}\nBalance : {Balance}\nStatus : {Status}";
        }
    }
}

