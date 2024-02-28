using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class BankEmployeeController : ControllerBase
    {
        private readonly ILogger<BankEmployeeController> _logger;
        private readonly IEmployeeAdminService _service;
        public BankEmployeeController(ILogger<BankEmployeeController> logger, IEmployeeAdminService service)
        {
            _logger = logger;
            _service = service;
        }

        
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<LoginDTO>> Register(EmpRegisterDTO empRegister)
        {
            try
            {
                var employee = await _service.Register(empRegister);
                _logger.LogInformation("Employee Registered");
                return employee;
            }
            catch(UserExistsException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginDTO>> Login(LoginDTO empLogin)
        {
            try
            {
                var employee = await _service.Login(empLogin);
                _logger.LogInformation("Employee LoggedIn");
                return employee;
            }
            catch (InvalidUserException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<BankEmployee>>> GetAll()
        {
            try
            {
                var employees = await _service.GetAllEmp();
                _logger.LogInformation("Employees Retrieved");
                return employees;
            }
            catch(NoBankEmployeeFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("GetByID")]
        [HttpGet]
        public async Task<ActionResult<BankEmployee>> GetBankEmployeeByIDAsync(int ID)
        {
            try
            {
                var employee = await _service.GetEmployeeByID(ID);
                _logger.LogInformation($"Employee with ID {ID} Retrieved");
                return employee;
            }
            catch (NoBankEmployeeFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<EmpUpdateDTO>> UpdateEmployeeAsync(EmpUpdateDTO employee)
        {
            try
            {
                employee = await _service.UpdateEmployee(employee);
                _logger.LogInformation("Employee Name and Position Updated");
                return employee;
            }
            catch (NoBankEmployeeFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<ActionResult<BankEmployee>> DeleteBankEmployeeAsync(int ID)
        {
            try
            {
                var employee = await _service.DeleteEmployee(ID);
                _logger.LogInformation("Employee Deleted");
                return employee;
            }
            catch (NoBankEmployeeFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

    }
}

