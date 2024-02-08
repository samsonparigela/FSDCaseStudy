using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class AddBenif
	{
		Beneficiaries benificiary;
		public AddBenif(AddOrUpdateBenifDTO benifDTO)
		{
			benificiary = new Beneficiaries();
			benificiary.BeneficiaryAccountNumber = benifDTO.BeneFiciaryNumber;
			benificiary.BeneficiaryName = benifDTO.BeneficiaryName;
			benificiary.IFSCCode = benifDTO.IFSCCode;
			benificiary.CustomerID = benifDTO.CustomerID;
		}

		public Beneficiaries GetBenif()
		{
			return benificiary;
		}
	}
}

