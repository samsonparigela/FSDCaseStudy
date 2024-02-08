using System;
using MavericksBank.Models;

namespace MavericksBank.Interfaces
{
	public interface ICustomerAdminService:ICustomerUserService
	{
        public Task<List<Customer>> GetAllCustomers();
        //public Task<Customer> GetCustomerByID(int key);
        
    }
}

