using System;
namespace MavericksBank.Models.DTO
{
	public class AccountStatusUpdateDTO
	{
        public int ID { set; get; }
        public string Status { set; get; } = string.Empty;
    }
}

