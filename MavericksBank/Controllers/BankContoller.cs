using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankContoller : ControllerBase
    {
        private readonly ILogger<BankContoller> _logger;
        private readonly IBankAdminService _service;
        public BankContoller(ILogger<BankContoller> logger, IBankAdminService service)
        {
            _logger = logger;
            _service = service;
        }


        [Route("Add")]
        [HttpPost]
        public async Task<BankCreateDTO> AddBankAsync(BankCreateDTO bank)
        {
            var myBank = await _service.AddBank(bank);
            _logger.LogInformation("Bank Created");
            return myBank;
        }


        [Route("GetAllBanks")]
        [HttpGet]
        public async Task<List<Banks>> GetAllBanksAsync()
        {
            var banks = await _service.GetAllBanks();
            _logger.LogInformation($"All Banks Retrieved");
            return banks;
        }

        [Route("GetByID")]
        [HttpGet]
        public async Task<Banks> GetBankByIDAsync(int ID)
        {
            var bank = await _service.GetBankbyID(ID);
            _logger.LogInformation($"Bank with ID {ID} Retrieved");
            return bank;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<BankUpdateDTO> UpdateBankAsync(BankUpdateDTO bank)
        {
            bank = await _service.UpdateBank(bank);
            _logger.LogInformation("Bank Updated");
            return bank;
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<Banks> DeleteAccountAsync(int ID)
        {
            var bank = await _service.DeleteBank(ID);
            _logger.LogInformation("Bank Deleted");
            return bank;
        }
    }
}

