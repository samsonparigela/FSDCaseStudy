using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface IEmployeeUserService
	{
        public Task<LoginDTO> Register(EmpRegisterDTO EmpRegister);
        public Task<LoginDTO> Login(LoginDTO EmpLogin);
        public Task<EmpUpdateDTO> UpdateEmployee(EmpUpdateDTO bankEmployee);
        public Task<BankEmployee> DeleteEmployee(int key);
        public Task<BankEmployee> GetEmployeeByID(int key);
    }
}

