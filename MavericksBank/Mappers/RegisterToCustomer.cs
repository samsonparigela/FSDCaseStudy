using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class RegisterToCustomer
	{
		Customer customer;
		public RegisterToCustomer(CustomerRegisterDTO customerRegister)
		{
			customer = new Customer();
			customer.Phone = customerRegister.Phone;
			customer.PANNumber = customerRegister.PANNumber;
			customer.Aadhaar = customerRegister.Aadhaar;
			customer.Name = customerRegister.Name;
			customer.DOB = customerRegister.DOB;
			customer.Age = customerRegister.Age;
			customer.Address = customerRegister.Address;
			customer.Gender = customerRegister.Gender;
		}
		public Customer GetCustomer()
		{
			return customer;
		}
	}
}

