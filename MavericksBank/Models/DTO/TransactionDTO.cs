using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class TransactionDTO
	{
		public int TransactionID { set; get; }
        public string Status { set; get; } = string.Empty;

        public int SAccountID { set; get; }
        public int BeneficiaryAccountNumber { set; get; }
        public string TransactionType { set; get; } = string.Empty;

        public float Amount { set; get; }

        public DateTime TransactionDate { set; get; }
        public string Description { set; get; } = string.Empty;
    }
}

