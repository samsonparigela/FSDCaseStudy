using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class LoanApplyDTO
	{
        public int LoanPolicyID { set; get; }
        public int CustomerID { set; get; }
        public string LoanPurpose { set; get; } = string.Empty;
        public float LoanAmount { set; get; }
    }
}

