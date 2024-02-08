using System;
namespace MavericksBank.Models.DTO
{
	public class CustomerPhoneAndAddressDTO
	{
		public int ID { set; get; }
		public string phoneNumber { set; get; } = string.Empty;
        public string Address { set; get; } = string.Empty;
    }
}

