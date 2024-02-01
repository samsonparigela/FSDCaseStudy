using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Beneficiaries:IEquatable<Beneficiaries>
	{
        public Beneficiaries(int beneficiaryID, int customerID, string beneficiaryName, int accountID,
            string bankName, string branch, string iFSCCode)
        {
            BeneficiaryID = beneficiaryID;
            CustomerID = customerID;
            BeneficiaryName = beneficiaryName;
            AccountID = accountID;
            BankName = bankName;
            Branch = branch;
            IFSCCode = iFSCCode;
        }

        [Key]
        public int BeneficiaryID { set; get; }

        [ForeignKey("CustomerID")]
        public int CustomerID { set; get; }

        public Customer Customer { set; get; }

        public string BeneficiaryName { set; get; }

        [ForeignKey("AccountID")]
        public int AccountID { set; get; }

        public Accounts Accounts { set; get; }


        public string BankName { set; get; }
        public string Branch { set; get; }
        public string IFSCCode { set; get; }

        public bool Equals(Beneficiaries? other)
        {
            return this.BeneficiaryID == other.BeneficiaryID;
        }

        public override string ToString()
        {
            return $"BeneficiaryID : {BeneficiaryID}\nCustomer : {Customer.CustomerID}\nBeneficiaryName : {BeneficiaryName}\n" +
            $"AccountNumber : {AccountID}\nBankName : {BankName}\nBranch : {Branch}\n" +
                $"IFSCCode : {IFSCCode}\n";
        }
    }
}

