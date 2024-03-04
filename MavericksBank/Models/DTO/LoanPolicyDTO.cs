using System;
namespace MavericksBank.Models.DTO
{
	public class LoanPolicyDTO
	{
		public LoanPolicyDTO()
		{

		}
        public int LoanPolicyID { get; set; }
        public int TenureInMonths { get; set; }
        public float Interest { get; set; }
        public float LoanAmount { get; set; }
    }
}

