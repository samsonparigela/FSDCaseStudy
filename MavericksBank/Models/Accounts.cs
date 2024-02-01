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
            float balance, string accountNumber,string status, int bankID)
        {
            AccountID = accountID;
            CustomerID = customerID;
            AccountType = accountType;
            Balance = balance;
            AccountNumber = accountNumber;
            Status = status;
            BankID = bankID;
        }

        [Key]
        public int AccountID { set; get; }
        public string AccountNumber { set; get; }

        [ForeignKey("CustomerID")]
        public int CustomerID { set; get; }
        public Customer Customer { set; get; }

        
        public int BankID { set; get; }
        [ForeignKey("BankID")]
        public Banks Banks { set; get; }


        
        public string Status { set; get; }
        public string AccountType { set; get; }
        public float Balance { set; get; }

        //Navigations

        //public Transactions SentTransactions { set; get; }
        //public Transactions RecievedTransactions { set; get; }

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

