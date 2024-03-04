using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface ICustomerUserService
	{
        public Task<LoginDTO> Register(CustomerRegisterDTO customerRegister);
        public Task<LoginDTO> Login(LoginDTO customerLogin);
        public Task<CustomerNameDTO> UpdateCustomerName(CustomerNameDTO customer);
        public Task<Customer> DeleteCustomer(int key);
        public Task<Customer> GetCustomerByID(int key);
    }
}

