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
        private readonly ITokenService _tokenService;

        public BankEmployeeService(ILogger<BankEmployeeService> logger, IRepository<BankEmployee, int> empRepo,
            IRepository<Users, string> usersRepo,ITokenService tokenService)
		{
            _logger = logger;
            _empRepo = empRepo;
            _usersRepo = usersRepo;
            _tokenService = tokenService;
		}

        public async Task<LoginDTO> Register(EmpRegisterDTO EmpRegister)
        {
            var user = new RegisterToEmpUser(EmpRegister).GetUser();
            var x = await _usersRepo.GetByID(user.UserName);
            if (x != null)
            {
                throw new UserExistsException("User Already Exists");
            }
            user = await _usersRepo.Add(user);            

            var employee = new RegisterToEmployee(EmpRegister).GetUser();
            employee.UserID = user.UserID;
            employee = await _empRepo.Add(employee);

            LoginDTO empLogin = new LoginDTO
            {
                UserName = EmpRegister.UserName,
                UserType = EmpRegister.UserType,
                userID = employee.UserID,
                Password = ""
            };

            return empLogin;
        }


        public async Task<LoginDTO> Login(LoginDTO EmpLogin)
        {
            var user =await _usersRepo.GetByID(EmpLogin.UserName);

            var employees = await _empRepo.GetAll();
            var emp = employees.Where(e => e.UserID == user.UserID).ToList().SingleOrDefault();

            if (user == null)
                throw new InvalidUserException();
            var password = getEncryptedPassword(EmpLogin.Password,user.Key);
            if(comparePasswords(password, user.Password))
            {
                _logger.LogInformation("Successfully Logged In");

                EmpLogin.Password = "";
                EmpLogin.UserType = user.UserType;
                EmpLogin.token = await _tokenService.GenerateToken(EmpLogin);
                EmpLogin.userID = emp.EmployeeID;
                return EmpLogin;
            }
            _logger.LogInformation("Successfully Logged In");
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

