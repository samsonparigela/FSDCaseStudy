using System;
using System.Security.Cryptography;
using System.Text;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MavericksBank.Services
{
	public class CustomerService:ICustomerAdminService
	{
        private readonly ILogger<CustomerService> _logger;
        private readonly IRepository<Customer,int> _CustomerRepo;
        private readonly IRepository<Users, string> _UserRepo;
        private readonly ITokenService _tokenService;

        public CustomerService(ILogger<CustomerService> logger, IRepository<Customer, int> CustomerRepo,
            IRepository<Users, string> usersRepo, ITokenService tokenService)
		{
            _logger = logger;
            _UserRepo = usersRepo;
            _CustomerRepo = CustomerRepo;
            _tokenService = tokenService;

        }

        public async Task<LoginDTO> Register(CustomerRegisterDTO customerRegister)
        {
            //var users = await _UserRepo.GetAll();
            var x = await _UserRepo.GetByID(customerRegister.UserName);
            //var user = users.Where(u => u.UserName == customerRegister.UserName).ToList();
            if(x!=null)
            {
                throw new UserExistsException("User Already Exists");
            }
            var myUser = new RegisterToUser(customerRegister).GetUser();
            myUser = await _UserRepo.Add(myUser);

            var myCustomer = new RegisterToCustomer(customerRegister).GetCustomer();
            myCustomer.UserID = myUser.UserID;
            myCustomer = await _CustomerRepo.Add(myCustomer);

            LoginDTO result = new LoginDTO
            {
                UserName = myUser.UserName,
                UserType = "Customer",
            };
            _logger.LogInformation("Successfully Registered");
            return result;
        }


        public async Task<LoginDTO> Login(LoginDTO customerLogin)
        {
            var user = await _UserRepo.GetByID(customerLogin.UserName);
            var customers = await _CustomerRepo.GetAll();
            var cust = customers.Where(c => c.UserID == user.UserID).ToList().SingleOrDefault();
            if (user == null)
                throw new InvalidUserException();
            var password = GetEncryptedPassword(customerLogin.Password,user.Key);
            if(ComparePasswords(password,user.Password))
            {
                customerLogin.Password = "";
                customerLogin.UserType = user.UserType;
                customerLogin.token = await _tokenService.GenerateToken(customerLogin);
                customerLogin.userID = cust.CustomerID;
            }
            else
                throw new InvalidUserException();
            _logger.LogInformation("Successfully Loggedin");
            return customerLogin;
        }

        public byte[] GetEncryptedPassword(String password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            byte[] userPassword= hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userPassword;
        }

        public bool ComparePasswords(byte[] pwd1, byte[] pwd2)
        {
            for(int i=0;i<pwd1.Length;i++)
            {
                if (pwd1[i] != pwd2[i])
                    return false;
            }
            return true;
        }

        public async Task<Customer> DeleteCustomer(int key)
        {
            var customer = await _CustomerRepo.GetByID(key);
            var userID = customer.UserID;
            customer = await _CustomerRepo.Delete(key);
            _logger.LogInformation($"Successfully Deleted Customer with ID : {key}");
            return customer;

        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers =await _CustomerRepo.GetAll();
            _logger.LogInformation("Successfully Retrieved all Customers");
            return customers;
        }

        public async Task<Customer> GetCustomerByID(int key)
        {
            var customer = await _CustomerRepo.GetByID(key);
            _logger.LogInformation($"Successfully Retrieved Customer with ID : {key}");
            return customer;
        }

        public async Task<CustomerNameDTO> UpdateCustomerName(CustomerNameDTO customer)
        {
            var user = await _CustomerRepo.GetByID(customer.ID);
            user.Name = customer.Name;
            user.Phone = customer.phoneNumber;
            user.Address = customer.Address;
            await _CustomerRepo.Update(user);
            _logger.LogInformation($"Successfully Updated Customer with ID : {customer.ID}");
            return customer;
        }

        public async Task<CustomerPhoneAndAddressDTO> UpdateCustomerPhoneAndAddress(CustomerPhoneAndAddressDTO customer)
        {
            var user = await _CustomerRepo.GetByID(customer.ID);
            user.Phone = customer.phoneNumber;
            user.Address = customer.Address;

            _logger.LogInformation($"Successfully Updated Customer Phone and Adress with ID : {customer.ID}");
            return customer;
        }
    }
}

