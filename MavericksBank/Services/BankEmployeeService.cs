using System;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Repository;
using MavericksBank.Mappers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;
using MavericksBank.Exceptions;

namespace MavericksBank.Services
{
	public class BankEmployeeService:IEmployeeAdminService
	{
        private readonly ILogger<BankEmployeeService> _logger;
        private readonly IRepository<BankEmployee,int> _empRepo;
        private readonly IRepository<Users,string> _usersRepo;
        public BankEmployeeService(ILogger<BankEmployeeService> logger, IRepository<BankEmployee, int> empRepo,
            IRepository<Users, string> usersRepo)
		{
            _logger = logger;
            _empRepo = empRepo;
            _usersRepo = usersRepo;
		}

        public async Task<EmpLoginDTO> Register(EmpRegisterDTO EmpRegister)
        {
            var user = new RegisterToEmpUser(EmpRegister).GetUser();
            user = await _usersRepo.Add(user);

            var employee = new RegisterToEmployee(EmpRegister).GetUser();
            employee.UserID = user.UserID;
            employee = await _empRepo.Add(employee);

            EmpLoginDTO empLogin = new EmpLoginDTO
            {
                UserName = EmpRegister.UserName,
                UserType = EmpRegister.UserType,
                Password = ""
            };

            return empLogin;
        }


        public async Task<EmpLoginDTO> Login(EmpLoginDTO EmpLogin)
        {
            var user =await _usersRepo.GetByID(EmpLogin.UserName);
            var password = getEncryptedPassword(EmpLogin.Password,user.Key);
            if(comparePasswords(password, user.Password))
            {
                EmpLogin.Password = "";
                EmpLogin.UserType = user.UserType;
                return EmpLogin;
            }

            throw new InvalidUserException();
        }

        public byte[] getEncryptedPassword(string password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            byte[] userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userPassword;
        }

        public bool comparePasswords(byte[] pwd1, byte[] pwd2)
        {
            for(int i=0;i<pwd1.Length;i++)
            {
                if (pwd1[i] != pwd2[i])
                    return false;
            }
            return true;
        }

        public async Task<BankEmployee> DeleteEmployee(int key)
        {
             var employee = await _empRepo.Delete(key);
            _logger.LogInformation($"Successfully Deleted Employee with ID : {key}");
            return employee;
        }

        public async Task<List<BankEmployee>> GetAllEmp()
        {
            var employees = await _empRepo.GetAll();
            _logger.LogInformation("Successfully Retrieved all Employees");
            return employees;
        }

        public async Task<BankEmployee> GetEmployeeByID(int key)
        {
            var employee = await _empRepo.GetByID(key);
            _logger.LogInformation($"Successfully Retrieved Employee with ID : {key}");
            return employee;
        }


        public async Task<EmpUpdateDTO> UpdateEmployee(EmpUpdateDTO bankEmployee)
        {
            var user = await _empRepo.GetByID(bankEmployee.ID);
            user.Name = bankEmployee.Name;
            user.Position= bankEmployee.position;
            await _empRepo.Update(user);
            _logger.LogInformation($"Successfully Updated Employee Name with ID : {bankEmployee.ID}");
            return bankEmployee;
        }
    }
}

