using System;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Mappers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MavericksBank.Services
{
	public class BankService:IBankAdminService
	{
        private readonly ILogger<BankService> _logger;
        private readonly IRepository<Banks, int> _BankRepo;
        public BankService(ILogger<BankService> logger, IRepository<Banks, int> BankRepo)
        {
            _logger = logger;
            _BankRepo = BankRepo;
        }

        public async Task<BankCreateDTO> AddBank(BankCreateDTO bank)
        {
            var myBank = new AddToBank(bank).GetBank();
            myBank = await _BankRepo.Add(myBank);
            _logger.LogInformation("Bank Created");
            return bank;
        }

        public async Task<Banks> DeleteBank(int Key)
        {
            var bank = await _BankRepo.Delete(Key);
            _logger.LogInformation($"Successfully Deleted Account : {Key}");
            return bank;
        }

        public async Task<List<Banks>> GetAllBanks()
        {
            var banks = await _BankRepo.GetAll();
            _logger.LogInformation($"Successfully Retrieved all banks");
            return banks;
        }

        public async Task<Banks> GetBankbyID(int ID)
        {
            var bank = await _BankRepo.GetByID(ID);
            _logger.LogInformation("Successfully Retrieved Bank");
            return bank;
        }

        public async Task<BankUpdateDTO> UpdateBank(BankUpdateDTO bank)
        {
            var myBank = await _BankRepo.GetByID(bank.ID);
            myBank.BankName = bank.BankName;
            await _BankRepo.Update(myBank);
            _logger.LogInformation($"Successfully Updated Bank with ID : {bank.ID}");
            return bank;

        }
    }
}

