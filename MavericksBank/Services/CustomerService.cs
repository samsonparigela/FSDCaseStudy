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

        public CustomerService(ILogger<CustomerService> logger, IRepository<Customer, int> CustomerRepo,
            IRepository<Users, string> usersRepo)
		{
            _logger = logger;
            _UserRepo = usersRepo;
            _CustomerRepo = CustomerRepo;
		}

        public async Task<CustomerLoginDTO> Register(CustomerRegisterDTO customerRegister)
        {
            var myUser = new RegisterToUser(customerRegister).GetUser();
            myUser = await _UserRepo.Add(myUser);

            var myCustomer = new RegisterToCustomer(customerRegister).GetCustomer();
            myCustomer.UserID = myUser.UserID;
            myCustomer = await _CustomerRepo.Add(myCustomer);

            CustomerLoginDTO result = new CustomerLoginDTO
            {
                UserName = myUser.UserName,
                UserType = myUser.UserType,
            };
            _logger.LogInformation("Successfully Registered");
            return result;
        }


        public async Task<CustomerLoginDTO> Login(CustomerLoginDTO customerLogin)
        {
            var user = await _UserRepo.GetByID(customerLogin.UserName);
            if (user == null)
                throw new InvalidUserException();
            var password = GetEncryptedPassword(customerLogin.Password,user.Key);
            if(ComparePasswords(password,user.Password))
            {
                customerLogin.Password = "";
                customerLogin.UserType = user.UserType;
            }
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
            await _CustomerRepo.Update(user);
            _logger.LogInformation($"Successfully Updated Customer Name with ID : {customer.ID}");
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

