using System;
namespace MavericksBank.Models.DTO
{
	public class CustomerLoginDTO
	{
        public string UserName { set; get; } = string.Empty;
        public string Password { set; get; } = string.Empty;
        public string UserType { set; get; } = string.Empty;
        public string token { set; get; } = string.Empty;
    }
}

