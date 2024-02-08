using System;
using MavericksBank.Models;

namespace MavericksBank.Interfaces
{
	public interface IEmployeeAdminService:IEmployeeUserService
	{
        public Task<List<BankEmployee>> GetAllEmp();
    }
}

