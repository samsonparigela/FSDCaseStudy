using System;
namespace MavericksBank.Models.DTO
{
	public class CustomerNameDTO
	{
        public int ID { set; get; }
        public string Name { set; get; } = string.Empty;
        public string phoneNumber { set; get; } = string.Empty;
        public string Address { set; get; } = string.Empty;
    }
}

