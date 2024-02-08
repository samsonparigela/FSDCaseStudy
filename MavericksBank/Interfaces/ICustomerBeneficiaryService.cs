using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface ICustomerBeneficiaryService
	{
		public Task<AddOrUpdateBenifDTO> AddBeneficiary(AddOrUpdateBenifDTO benifDTO);
        public Task<Beneficiaries> DeleteBeneficiary(int ID);
        public Task<List<Beneficiaries>> GetAllBeneficiary(int CID);
        public Task<Beneficiaries> GetBeneficiaryByID(int BID);
        public Task<AddOrUpdateBenifDTO> UpdateBeneficiary(AddOrUpdateBenifDTO benifDTO);

    }
}

