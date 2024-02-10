using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Transactions:IEquatable<Transactions>
	{
        public Transactions()
        {

        }

        public Transactions(int transactionID,
    string transactionType, float amount,
    DateTime transactionDate, string description,int sAccountID,int beneficiaryAccountNumber, string status)
        {
            TransactionID = transactionID;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            Amount = amount;
            Description = description;
            SAccountID = sAccountID;
            BeneficiaryAccountNumber = beneficiaryAccountNumber;
            Status = status;
        }

        [Key]
        public int TransactionID { set; get; }
        public string Status { set; get; }

        [ForeignKey("SAccountID")]
        public int SAccountID { set; get; }
        public Accounts? SourceAccount { set; get; }

        [ForeignKey("BeneficiaryID")]
        public int BeneficiaryAccountNumber { set; get; }
        public Beneficiaries? Beneficiaries { set; get; }


        public string TransactionType { set; get; }

        public float Amount { set; get; }
        
        public DateTime TransactionDate { set; get; }
        public string Description { set; get; }

        public override string ToString()
        {
            return $"TransactionID : {TransactionID}\n" +
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

