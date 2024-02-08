using System;
namespace MavericksBank.Models.DTO
{
	public class LoanExtendDTO
	{
		public int LoanID { set; get; }
		public int TenureInMonths { set; get; }
		public string Status = "Pending";
	}
}

