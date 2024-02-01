using System;
using System.ComponentModel.DataAnnotations;

namespace MavericksBank.Models
{
	public class Banks:IEquatable<Banks>
	{
        public Banks(int bankID, string bankName)
        {
            BankID = bankID;
            BankName = bankName;
        }

        [Key]
        public int BankID { get; set; }
		public string BankName { get; set; }




        public bool Equals(Banks? other)
        {
            return this.BankID == other.BankID;
        }

        public override string ToString()
        {
            return $"BankID : {BankID}\nBankName : {BankName}";
        }
    }
}

