using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models.DTO
{
	public class AccountsCreateDTO
	{
        public string AccountNumber { set; get; } = string.Empty;


        public string IFSCCode { set; get; } = string.Empty;

        public string AccountType { set; get; } = string.Empty;

        public float Balance { set; get; }
        public int customerID { set; get; }
    }
}

