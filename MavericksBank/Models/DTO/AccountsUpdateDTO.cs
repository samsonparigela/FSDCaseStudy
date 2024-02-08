using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class AccountsUpdateDTO
	{
        public int ID { set; get; }
        public string AccountNumber { set; get; } = string.Empty;

        public string IFSCCode { set; get; } = string.Empty;

        public string AccountType { set; get; } = string.Empty;
    }
}

