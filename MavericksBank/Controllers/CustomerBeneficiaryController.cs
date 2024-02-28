using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class CustomerBeneficiaryController : Controller
    {
        private readonly ILogger<CustomerBeneficiaryController> _logger;
        private readonly ICustomerBeneficiaryService _service;
        public CustomerBeneficiaryController(ILogger<CustomerBeneficiaryController> logger, ICustomerBeneficiaryService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [Route("AddBeneficiary")]
        [HttpPost]
        public async Task<ActionResult<AddOrUpdateBenifDTO>> AddBeneficiary(AddOrUpdateBenifDTO benifDTO)
        {
            try
            {
                var benif = await _service.AddBeneficiary(benifDTO);
                _logger.LogInformation("Beneficiary Added");
                return benif;
            }
            catch(BeneficiaryAlreadyPresent ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("DeleteBeneficiary")]
        [HttpDelete]
        public async Task<ActionResult<Beneficiaries>> DeleteBeneficiary(int ID)
        {
            try
            {
                var benif = await _service.DeleteBeneficiary(ID);
                _logger.LogInformation("Beneficiary Deleted");
                return benif;
            }
            catch (NoBeneficiariesFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("GetAllBeneficiary")]
        [HttpGet]
        public async Task<ActionResult<List<Beneficiaries>>> GetAllBeneficiary(int CID)
        {
            try
            {
                var benif = await _service.GetAllBeneficiary(CID);
                _logger.LogInformation("Beneficiaries Retrieved");
                return benif;
            }
            catch (NoBeneficiariesFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("GetBeneficiaryByID")]
        [HttpGet]
        public async Task<ActionResult<Beneficiaries>> GetBeneficiaryByID(int BID)
        {
            try
            {
                var benif = await _service.GetBeneficiaryByID(BID);
                _logger.LogInformation("Beneficiary Added");
                return benif;
            }
            catch (NoBeneficiariesFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("UpdateBeneficiary")]
        [HttpPut]
        public async Task<ActionResult<AddOrUpdateBenifDTO>> UpdateBeneficiary(AddOrUpdateBenifDTO benifDTO)
        {
            try
            {
                var benif = await _service.UpdateBeneficiary(benifDTO);
                _logger.LogInformation("Beneficiary Updated");
                return benif;
            }
            catch (NoBeneficiariesFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}

