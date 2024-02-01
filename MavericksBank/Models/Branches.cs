using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Branches:IEquatable<Branches>
	{

        public Branches(string iFSCCode, string branchName,int bankID)
        {
            IFSCCode = iFSCCode;
            BranchName = branchName;
            BankID = bankID;
        }


        
        [Key]
        public string IFSCCode { get; set; }
		public string BranchName { get; set; }

        
        public int BankID { get; set; }
        [ForeignKey("BankID")]
        public Banks Banks { get; set; }



        public bool Equals(Branches? other)
        {
            return this.BankID == other.BankID;
        }

        public override string ToString()
        {
            return $"IFSC Code : {IFSCCode}\nBranchName : {BranchName}";
        }
    }
}

