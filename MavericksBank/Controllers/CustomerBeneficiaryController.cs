using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBeneficiaryController : Controller
    {
        private readonly ILogger<CustomerBeneficiaryController> _logger;
        private readonly ICustomerBeneficiaryService _service;
        public CustomerBeneficiaryController(ILogger<CustomerBeneficiaryController> logger, ICustomerBeneficiaryService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("Add Beneficiary")]
        [HttpPost]
        public async Task<AddOrUpdateBenifDTO> AddBeneficiary(AddOrUpdateBenifDTO benifDTO)
        {
            var benif = await _service.AddBeneficiary(benifDTO);
            _logger.LogInformation("Beneficiary Added");
            return benif;
        }

        [Route("Delete Beneficiary")]
        [HttpDelete]
        public async Task<Beneficiaries> DeleteBeneficiary(int ID)
        {
            var benif = await _service.DeleteBeneficiary(ID);
            _logger.LogInformation("Beneficiary Deleted");
            return benif;
        }

        [Route("Get All Beneficiary")]
        [HttpGet]
        public async Task<List<Beneficiaries>> GetAllBeneficiary(int CID)
        {
            var benif = await _service.GetAllBeneficiary(CID);
            _logger.LogInformation("Beneficiaries Retrieved");
            return benif;
        }

        [Route("Get Beneficiary By ID")]
        [HttpGet]
        public async Task<Beneficiaries> GetBeneficiaryByID(int BID)
        {
            var benif = await _service.GetBeneficiaryByID(BID);
            _logger.LogInformation("Beneficiary Added");
            return benif;
        }

        [Route("Update Beneficiary")]
        [HttpPut]
        public async Task<AddOrUpdateBenifDTO> UpdateBeneficiary(AddOrUpdateBenifDTO benifDTO)
        {
            var benif = await _service.UpdateBeneficiary(benifDTO);
            _logger.LogInformation("Beneficiary Updated");
            return benif;
        }
    }
}

