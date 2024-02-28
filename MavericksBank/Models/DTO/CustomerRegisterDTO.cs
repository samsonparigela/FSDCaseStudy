using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class CustomerRegisterDTO
	{

        public string UserName { set; get; } = string.Empty;
        public string Password { set; get; } = string.Empty;
        public string UserType = "Customer";
        public string Name { set; get; } = string.Empty;
        public DateTime DOB { set; get; }
        public int Age { set; get; }
        public string Phone { set; get; } = string.Empty;
        public string Address { set; get; } = string.Empty;
        public string Gender { set; get; } = string.Empty;
        public string Aadhaar { set; get; } = string.Empty;
        public string PANNumber { set; get; } = string.Empty;

    }
}

