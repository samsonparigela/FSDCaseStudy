using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<EmpLoginDTO> Register(EmpRegisterDTO empRegister)
        {
            var employee = await _service.Register(empRegister);
            _logger.LogInformation("Employee Registered");
            return employee;
        }

        
        [Route("Login")]
        [HttpPost]
        public async Task<EmpLoginDTO> Login(EmpLoginDTO empLogin)
        {
            var employee = await _service.Login(empLogin);
            _logger.LogInformation("Employee LoggedIn");
            return employee;
        }

        
        [Route("GetAll")]
        [HttpGet]
        public async Task<List<BankEmployee>> GetAll()
        {
            var employees= await _service.GetAllEmp();
            _logger.LogInformation("Employees Retrieved");
            return employees;
        }

        [Route("GetByID")]
        [HttpGet]
        public async Task<BankEmployee> GetBankEmployeeByIDAsync(int ID)
        {
            var employee = await _service.GetEmployeeByID(ID);
            _logger.LogInformation($"Employee with ID {ID} Retrieved");
            return employee;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<EmpUpdateDTO> UpdateEmployeeAsync(EmpUpdateDTO employee)
        {
            employee = await _service.UpdateEmployee(employee);
            _logger.LogInformation("Employee Name and Position Updated");
            return employee;
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<BankEmployee> DeleteBankEmployeeAsync(int ID)
        {
            var employee = await _service.DeleteEmployee(ID);
            _logger.LogInformation("Employee Deleted");
            return employee;
        }

    }
}

