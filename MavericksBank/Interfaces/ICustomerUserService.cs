using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface ICustomerUserService
	{
        public Task<CustomerLoginDTO> Register(CustomerRegisterDTO customerRegister);
        public Task<CustomerLoginDTO> Login(CustomerLoginDTO customerLogin);
        public Task<CustomerNameDTO> UpdateCustomerName(CustomerNameDTO customer);
        public Task<CustomerPhoneAndAddressDTO> UpdateCustomerPhoneAndAddress(CustomerPhoneAndAddressDTO customer);
        public Task<Customer> DeleteCustomer(int key);
        public Task<Customer> GetCustomerByID(int key);
    }
}

