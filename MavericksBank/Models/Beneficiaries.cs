using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Beneficiaries:IEquatable<Beneficiaries>
	{
        public Beneficiaries(int beneficiaryID, int customerID, string beneficiaryName,
            string iFSCCode)
        {
            BeneficiaryAccountNumber = beneficiaryID;
            CustomerID = customerID;
            BeneficiaryName = beneficiaryName;
            IFSCCode = iFSCCode;
        }
        public Beneficiaries()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BeneficiaryAccountNumber { set; get; }

        [ForeignKey("CustomerID")]
        public int CustomerID { set; get; }

        public Customer? Customer { set; get; }
        public List<Transactions> Transactions { set; get; }

        public string BeneficiaryName { set; get; }

        [ForeignKey("IFSCCode")]
        public string IFSCCode { set; get; }

        public Branches Branch { set; get; }



        public bool Equals(Beneficiaries? other)
        {
            return this.BeneficiaryAccountNumber == other.BeneficiaryAccountNumber;
        }

        public override string ToString()
        {
            return $"\nCustomer : {Customer.CustomerID}\nBeneficiaryName : {BeneficiaryName}\n";
        }
    }
}

