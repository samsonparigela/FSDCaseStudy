using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Transactions:IEquatable<Transactions>
	{
        public Transactions(int transactionID,
    string transactionType, float amount, int sAccountID,
    DateTime transactionDate, string description)
        {
            TransactionID = transactionID;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            Amount = amount;
            Description = description;
            SAccountID = sAccountID;
            //DAccountID = dAccountID;
            
            
        }

        [Key]
        public int TransactionID { set; get; }




        public int SAccountID;
        [ForeignKey("SAccountID")]
        public Accounts SourceAccount { set; get; }


        public string TransactionType { set; get; }

        public float Amount { set; get; }
        
        public DateTime TransactionDate { set; get; }
        public string Description { set; get; }

        public override string ToString()
        {
            return $"TransactionID : {TransactionID}\nSourceAccount : {SourceAccount.AccountID}\n" +
                $"TransactionType : {TransactionType}\nAmount : {Amount}\n" +
            $"\nTransactionDate : {TransactionDate}\n" +
            $"Description : {Description}";
        }

        public bool Equals(Transactions? other)
        {
            return other.TransactionID == this.TransactionID;
        }
    }
}

