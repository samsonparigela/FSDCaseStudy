using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class AddToBank
	{
		Banks bank;
		public AddToBank(BankCreateDTO bankCreate)
		{
			bank = new Banks();
			bank.BankName = bankCreate.BankName;
		}
		public Banks GetBank()
		{
			return bank;
		}
	}
}

