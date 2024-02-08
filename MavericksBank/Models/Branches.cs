using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Branches:IEquatable<Branches>
	{

        public Branches()
        {

        }
        public Branches(string iFSCCode, string branchName,int bankID, string city)
        {
            IFSCCode = iFSCCode;
            BranchName = branchName;
            BankID = bankID;
            City = city;
        }



        [Key]
        public string IFSCCode { get; set; } = string.Empty;
		public string BranchName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public int BankID { get; set; }
        [ForeignKey("BankID")]
        public Banks? Banks { get; set; }

        public List<Accounts>? Accounts { set; get; }
        public List<Beneficiaries>? Beneficiaries { set; get; }
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

