using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class EmpRegisterDTO
	{
        public string UserName { set; get; } = string.Empty;
        public string Password { set; get; } = string.Empty;
        public string Name { set; get; } = string.Empty;
        public string Position { set; get; } = string.Empty;
        public string UserType = "Bank Employee";
    }
}

