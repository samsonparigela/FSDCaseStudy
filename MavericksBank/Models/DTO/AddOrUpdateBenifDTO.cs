using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class AddOrUpdateBenifDTO
	{
        public int CustomerID { set; get; }
        public int BeneFiciaryNumber { set; get; }
        public string BeneficiaryName { set; get; } = string.Empty;
        public string IFSCCode { set; get; } = string.Empty;
    }
}

