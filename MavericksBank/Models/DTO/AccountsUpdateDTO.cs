using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class AccountsUpdateDTO
	{
        public int ID { set; get; }
        public int AccountNumber { set; get; }

        public string IFSCCode { set; get; } = string.Empty;

        public string AccountType { set; get; } = string.Empty;
    }
}

