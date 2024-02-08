using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class RegisterToEmployee
	{
		BankEmployee employee;
		public RegisterToEmployee(EmpRegisterDTO empRegister)
		{
			employee = new BankEmployee();
			employee.Name = empRegister.Name;
			employee.Position = empRegister.Position;
        }
		public BankEmployee GetUser()
		{
			return employee;
		}
	}
}

