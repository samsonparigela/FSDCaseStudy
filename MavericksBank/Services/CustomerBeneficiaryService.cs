using System;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Services
{
	public class CustomerBeneficiaryService:ICustomerBeneficiaryService
	{
        private readonly ILogger<CustomerBeneficiaryService> _logger;
        private readonly IRepository<Beneficiaries, int> _benefRepo;
        public CustomerBeneficiaryService(ILogger<CustomerBeneficiaryService> logger, IRepository<Beneficiaries, int> benefRepo)
		{
            _logger = logger;
            _benefRepo = benefRepo;
		}

        public async Task<AddOrUpdateBenifDTO> AddBeneficiary(AddOrUpdateBenifDTO benifDTO)
        {
            var benifs = await _benefRepo.GetAll();
            var benif = benifs.Where(a => a.BeneficiaryAccountNumber == benifDTO.BeneFiciaryNumber);
            //if (benif != null)
            //    throw new BeneficiaryAlreadyPresent("Beneficiary ID is already present");
            var beneficiary = new AddBenif(benifDTO).GetBenif();

            await _benefRepo.Add(beneficiary);
            _logger.LogInformation("Beneficiary Added");
            return benifDTO;
        }

        public async Task<Beneficiaries> DeleteBeneficiary(int ID)
        {
            var benificiary = await _benefRepo.Delete(ID);
            _logger.LogInformation("Beneficiary Deleted");
            return benificiary;

        }

        public async Task<List<Beneficiaries>> GetAllBeneficiary(int CID)
        {
            var benificiaries = await _benefRepo.GetAll();
            benificiaries = benificiaries.Where(b => b.CustomerID == CID).ToList();
            _logger.LogInformation("Beneficiaries Retrieved");
            return benificiaries;
        }

        public async Task<Beneficiaries> GetBeneficiaryByID(int BID)
        {
            var benificiary = await _benefRepo.GetByID(BID);
            _logger.LogInformation("Beneficiary Retrieved");
            return benificiary;
        }

        public async Task<AddOrUpdateBenifDTO> UpdateBeneficiary(AddOrUpdateBenifDTO benifDTO)
        {
            var beneficiary = new AddBenif(benifDTO).GetBenif();
            await _benefRepo.Update(beneficiary);
            _logger.LogInformation("Beneficiary Updated");
            return benifDTO;
        }
    }
}

